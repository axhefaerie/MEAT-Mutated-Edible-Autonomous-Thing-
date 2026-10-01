using UnityEngine;

public class ProyectileAttackComponent : MonoBehaviour, IAttackComponent
{
    //public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Offsets")]
    public Vector3 bulletOriginOffset = new Vector3(0, -1f, 0);

    [Header("Bullet Settings")]
    public float bulletSpeed = 10f;
    public float bulletGravity = 0f; // 0 = sin caída, 1 = normal
    public int bulletDamage = 1;
    public float attackCooldown = 1f; // tiempo mínimo entre disparos

    private float lastAttackTime = 0f; // tiempo del último disparo

    public void Attack()
    {
        // Chequear cooldown
        if (Time.time < lastAttackTime + attackCooldown)
            return; // aún no ha pasado el tiempo, no dispara

        // Actualizar último disparo
        lastAttackTime = Time.time;

        // Dirección horizontal del enemigo
        float dirX = Mathf.Sign(transform.localScale.x); // 1 = derecha, -1 = izquierda

        // Invertir el offset X si mira a la derecha
        Vector3 appliedOffset = bulletOriginOffset;
        if (dirX > 0) // mirando a la derecha
        {
            appliedOffset.x *= -1f;
        }

        // Posición final de spawn
        Vector3 spawnPos = firePoint.position + appliedOffset;

        // Pedir una bala del pool
        GameObject bullet = BulletPool.Instance.RequestBullet();
        bullet.transform.position = spawnPos;
        bullet.transform.rotation = Quaternion.identity;

        // Configurar componente de la bala
        BulletComponent bulletScript = bullet.GetComponent<BulletComponent>();
        bulletScript.damage = bulletDamage;
        bulletScript.speed = bulletSpeed;
        bulletScript.SetDirection(new Vector3(dirX, 0, 0)); // disparo horizontal
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = bulletGravity;
        }
    }

}