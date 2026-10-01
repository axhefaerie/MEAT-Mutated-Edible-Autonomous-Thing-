using System.Collections;
using UnityEngine;

public class TakeDamageComponent : MonoBehaviour, ITakeDamageComponent
{
    public Enemy enemy;
    public int currentHealth;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Duración del flash en segundos
    public float flashDuration = 0.2f;

    public AudioClip hitSound;
    public AudioClip dieSound;

    public EnemyHealthUI healthUI;

    void Awake()
    {
        if (enemy == null)
            enemy = GetComponentInParent<Enemy>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void Start()
    {
        currentHealth = enemy.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            

            Bullet bullet = collision.GetComponent<Bullet>();

            if (bullet != null) { 
                AudioManager.Instance.PlaySFX(hitSound);
                TakeDamage(bullet.Damage);
            }

            Destroy(collision.gameObject);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Mostrar barra de vida la primera vez y actualizarla
        if (healthUI != null)
            healthUI.OnHit();

        // Lanzamos el flash
        StartCoroutine(FlashEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashEffect()
    {
        if (spriteRenderer != null)
        {
            // Se pone rojo para dar feedback
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(flashDuration);

            // Volver al color original
            spriteRenderer.color = originalColor;
        }
    }

    private void Die()
    {

        // Ocultar barra de vida si existe
        EnemyHealthUI healthUI = GetComponentInChildren<EnemyHealthUI>();
        if (healthUI != null)
            healthUI.HideHealthBar();

        AudioManager.Instance.PlaySFX(dieSound);
        GameManager.Instance.EnemyKilled();

        Destroy(gameObject);
    }
}
