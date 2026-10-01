using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Duración del flash en segundos
    public float flashDuration = 0.2f;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            if (bullet != null)
                TakeDamage(bullet.Damage);   // <<<<< ahora usa el daño real

            collision.gameObject.SetActive(false);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

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
        Destroy(gameObject);
    }
}
