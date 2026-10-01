using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    public Enemy enemy;       // Referencia al enemigo
    public GameObject healthCanvas;         // Canvas que contiene la barra de vida
    public Image healthBar;             // Image con tipo Fill

    private bool shown = false;

    public TakeDamageComponent takeDamageComponent;

    void Start()
    {
        if (healthCanvas != null)
            healthCanvas.gameObject.SetActive(false); // Inicialmente invisible
    }

    // Llamar a esto cuando el enemigo reciba daño
    public void OnHit()
    {
        if (!shown)
        {
            shown = true;
            healthCanvas.gameObject.SetActive(true); // Mostrar la barra la primera vez
        }

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (healthBar != null && takeDamageComponent != null)
        {
            healthBar.fillAmount = Mathf.Clamp01(
                (float)takeDamageComponent.currentHealth / (float)enemy.health
            );
        }
    }

    public void HideHealthBar()
    {
        if (healthCanvas != null)
            healthCanvas.SetActive(false);
    }
}
