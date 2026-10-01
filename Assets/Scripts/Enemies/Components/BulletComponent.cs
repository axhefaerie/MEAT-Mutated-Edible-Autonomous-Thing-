using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    private Vector3 direction;
    private Rigidbody2D rb;

    [Header("Sound effects")]
    public AudioClip hitSound;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // RESET TOTAL AL SACAR DEL POOL
        direction = Vector3.zero;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    // Establecer direccion de disparo
    public void SetDirection (Vector3 dir)
    {
        direction = dir.normalized;

        // Actualizar velocidad si tiene Rigidbody2D
        if (rb != null)
            rb.linearVelocity = direction * speed;
    }

    // Detectar colisiones
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {
                for (int i = 0; i < damage; i++) { 
                    GameManager.Instance.LooseLife(damage);
                    AudioManager.Instance.PlaySFX(hitSound);
                }
            gameObject.SetActive(false);
            }
        else if (collision.CompareTag("Ground"))
        {
            // Destruir la bala al chocar con suelo o pared
            gameObject.SetActive(false);
        }
    }
}
