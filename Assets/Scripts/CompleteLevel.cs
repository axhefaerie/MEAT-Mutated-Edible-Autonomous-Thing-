using UnityEngine;

public class CompleteLevel : MonoBehaviour
{

    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] meatPieces = GameObject.FindGameObjectsWithTag("MeatPiece");

        // Aún no puede completar el nivel
        if (enemies.Length > 0 || meatPieces.Length > 0)
        {
            HUD hud = FindObjectOfType<HUD>();
            if (hud != null)
                hud.ShowEnemiesRemaining(enemies.Length);

            return;
        }

        // Nivel completado
        AudioManager.Instance.PlaySFX(sound);
        Destroy(gameObject);
    }

}
