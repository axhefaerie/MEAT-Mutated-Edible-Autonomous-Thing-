using UnityEngine;

public class LilMush : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    private int damage = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LooseLife(damage);
        }
    }
}
