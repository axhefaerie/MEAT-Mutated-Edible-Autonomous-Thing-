using UnityEngine;

using UnityEngine;

public class CollisionComponent : MonoBehaviour, ICollisionComponent
{
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ProcessCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LooseLife(1);
        }

        if (other.gameObject.CompareTag("Ground"))
        {

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        ProcessCollision(other.gameObject);
    }
}



