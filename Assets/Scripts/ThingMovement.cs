using UnityEngine;
using System.Collections;

public class ThingMovement : MonoBehaviour
{
    [Header("Payer setinggs")]
    public float Speed;
    public float JumpForce;
    public Vector3 startPosition;

    [Header("Bullet setinggs")]
    public GameObject Bullet;
    public Transform firePointUp;
    public Transform firePointDown;
    public Vector2 shootDirectionUp = new Vector2(1f, 0.7f);
    public Vector2 shootDirectionDown = new Vector2(1f, 0f);
    public int BulletDamage = 1;
    public float fireCooldown = 0.5f;   

    [Header("SOUNDS")]
    private bool runningSoundPlaying = false;
    public AudioClip RunnningSound;
    public AudioClip JumpSound;
    public AudioClip ShootSound;
    public AudioClip WaterSound;

    private enum ShootType { None, Up, Down }
    private ShootType currentShootType = ShootType.None;

    private Rigidbody2D rb;
    private Animator animator;
    private float horizontal;
    private float lastShoot;

    private int jumps = 1;
    private int maxJumps = 1;
    private bool isRespawning = false;

    bool isGrounded;

    // Knockback
    private bool isKnockbacked = false;
    private float knockbackTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumps = maxJumps;

        startPosition = transform.position;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0)
        {          
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        }
        else if (horizontal > 0) {
            
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }

        animator.SetBool("running", horizontal != 0);
        HandleRunningSound();

        if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
        {
            Jump();
            jumps--;
            animator.SetTrigger("jump");
            Debug.Log("Jump used. Jumps left = " + jumps);
        }

        // DISPARO ABAJO (E)
        if (Input.GetKeyDown(KeyCode.E) && CanShoot())
        {
            currentShootType = ShootType.Down;
            animator.SetTrigger("attack");
            lastShoot = Time.time;
        }

        // DISPARO ARRIBA (Q)
        if (Input.GetKeyDown(KeyCode.Q) && CanShoot())
        {
            currentShootType = ShootType.Up;
            animator.SetTrigger("attack");
            lastShoot = Time.time;
        }

    }

    private bool CanShoot()
    {
        return Time.time >= lastShoot + fireCooldown;
    }

    private void HandleRunningSound()
    {
        bool isMoving = horizontal != 0 && isGrounded && !isKnockbacked;

        AudioSource sfx = AudioManager.Instance.sfxSource;

        if (isMoving && !runningSoundPlaying)
        {
            sfx.clip = RunnningSound;
            sfx.loop = true;
            sfx.Play();
            runningSoundPlaying = true;
        }
        else if (!isMoving && runningSoundPlaying)
        {
            sfx.Stop();
            sfx.clip = null;
            runningSoundPlaying = false;
        }
    }

    public void StopRunningSound()
    {
        if (!runningSoundPlaying) return;

        AudioSource sfx = AudioManager.Instance.sfxSource;
        sfx.Stop();
        sfx.clip = null;
        runningSoundPlaying = false;
    }

    private void TryReactivateRunningSound()
    {
        HandleRunningSound();
    }

    private void Jump()
    {
        isGrounded = false;

        StopRunningSound();
        AudioManager.Instance.PlaySFX(JumpSound);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (isKnockbacked) { return; }
        rb.linearVelocity = new Vector2(horizontal * Speed, rb.linearVelocity.y);
    }

    public void ApplyKnockback(Vector2 force)
    {
        isKnockbacked = true;
        knockbackTimer = 0.2f; // duración del empujón

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);

        Invoke(nameof(EndKnockback), knockbackTimer);
    }

    private void EndKnockback()
    {
        isKnockbacked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                animator.SetTrigger("land");
                jumps = maxJumps;
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRespawning) return;

        if (collision.CompareTag("Water"))
        {
            isRespawning = true;
            AudioManager.Instance.PlaySFX(WaterSound);
            StartCoroutine(RespawnDelayed(0.5f));
        }
    }

    private IEnumerator RespawnDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Respawn();
        isRespawning = false;
    }

    private void Respawn()
    {
        // Teletransportar al inicio
        transform.position = startPosition;

        // Resetear velocidad y saltos
        rb.linearVelocity = Vector2.zero;
        jumps = maxJumps;

        // Reset animaciones si quieres
        animator.SetTrigger("land"); // opcional, para que se vea �parado�
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Salio de contacto con Ground");
        }
    }

    private void Shoot()
    {
        AudioManager.Instance.PlaySFX(ShootSound);
        animator.SetTrigger("attack");
    }

    public void ShootDown_AnimEvent()
    {
        if (currentShootType != ShootType.Down) return;

        Vector2 dir = shootDirectionDown;
        dir.x *= Mathf.Sign(transform.localScale.x);

        FireBullet(firePointDown, dir);
    }

    public void ShootUp_AnimEvent()
    {
        if (currentShootType != ShootType.Up) return;

        Vector2 dir = shootDirectionUp;

        // Respeta el lado al que mira el jugador
        dir.x *= Mathf.Sign(transform.localScale.x);

        FireBullet(firePointUp, dir);
    }

    private void FireBullet(Transform firePoint, Vector2 direction)
    {
        GameObject bullet = Instantiate(
        Bullet,
        firePoint.position,
        Quaternion.identity
    );

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(direction.normalized);
        bulletScript.Damage = BulletDamage;

        AudioManager.Instance.PlaySFX(ShootSound);

        currentShootType = ShootType.None;
    }
}
