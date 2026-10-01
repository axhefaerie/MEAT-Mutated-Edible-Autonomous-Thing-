using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float Speed;
    public int Damage = 1;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * Speed;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si choca con enemigo
        TakeDamageComponent enemyDamage = collision.GetComponent<TakeDamageComponent>();
        if (enemyDamage != null)
        {
            enemyDamage.TakeDamage(Damage);
            gameObject.SetActive(false);   //Destroy(gameObject); SI NO FUNCIONAN LAS BALAS DE LOS ENEMIGOS O ALGO CAMBIAR ESTA LINEA POR EL COMENTARIO
            return;
        }

        // Si choca con suelo u obstaculo
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
