using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speedMultiplier = 1.5f;
    public int extraDamage = 1;

    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ThingMovement player = collision.GetComponent<ThingMovement>();

        if (player != null)
        {
            StartCoroutine(ApplyPowerUp(player));
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private IEnumerator ApplyPowerUp(ThingMovement player)
    {
        // Guardar valores originales
        float originalSpeed = player.Speed;
        int originalDamage = player.BulletDamage;

        // Efectos
        player.Speed *= speedMultiplier;
        player.BulletDamage += extraDamage;

        yield return new WaitForSeconds(duration);

        // Restaurar valores
        player.Speed = originalSpeed;
        player.BulletDamage = originalDamage;

        // Destruir power-up
        Destroy(gameObject);
    }
}
