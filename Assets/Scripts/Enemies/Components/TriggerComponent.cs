using UnityEngine;

public class TriggerComponent : MonoBehaviour
{
    private bool hasHit = false;
    private int damage = 1;
    private Collider2D col;
    [SerializeField] public float knockbackForce = 5f;

    public AudioClip sound;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!col.enabled) return;
        if (hasHit) { return; }

        if (other.CompareTag("Player"))
        {
           AudioManager.Instance.PlaySFX(sound);
           GameManager.Instance.LooseLife(damage);
           hasHit = true;
            // Knockback
            ThingMovement player = other.GetComponent<ThingMovement>();
            if (player != null)
            {
                Vector2 direction = (other.transform.position - transform.position).normalized;
                player.ApplyKnockback(direction * knockbackForce);
            }
        }
    }

    public void ResetHit()
    {
        hasHit = false;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

}
