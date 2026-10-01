using UnityEngine;

public class EnemyUnlockTrigger : MonoBehaviour
{
    [Header("Enemy ID")]
    public int levelNumber;     // Nivel al que pertenece
    public int enemyID;         // ID unico dentro del nivel

    private bool activated = false;

    private string PrefKey => $"Enemy_{levelNumber}_{enemyID}";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            // Guardar desbloqueo
            PlayerPrefs.SetInt(PrefKey, 1);
            PlayerPrefs.Save();

            Debug.Log($"Enemy unlocked: {PrefKey}");

            // Opcional: destruir el trigger
            Destroy(gameObject);
        }
    }

    public static bool IsEnemyUnlocked(int level, int enemyID)
    {
        string key = $"Enemy_{level}_{enemyID}";
        return PlayerPrefs.GetInt(key, 0) == 1;
    }
}
