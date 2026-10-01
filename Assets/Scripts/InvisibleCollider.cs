using UnityEngine;

public class InvisibleCollider : MonoBehaviour
{

    public GameObject Mush;

    private bool colision = false;  
    private int numSpawns = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisión detectada con: " + other.name);

        // verificamos que el que entra sea el jugador
        if (other.CompareTag("Player") && numSpawns == 0)
        {
            Debug.Log("Es el Player!");

            colision = true;
            SpawnMush();
            numSpawns ++;
        }
    }

    private void SpawnMush()
    {
        Instantiate(Mush, transform.position + new Vector3(5.0f, -2.0f, -3.0f), Quaternion.identity);
    }
}
