using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LooseLife(1);
        }
    }
}
