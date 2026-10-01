using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnOffsetX = 10f;

    private bool activated = false;

    public AudioClip spawnSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (!activated && other.CompareTag("Player"))
        {
            //enemyToSpawn.SetActive(true);
            AudioManager.Instance.PlaySFX(spawnSound);

            activated = true;

            // Collider pos
            Vector3 spawnPos = transform.position;
            spawnPos.x += spawnOffsetX;

            enemyToSpawn.transform.position = spawnPos;
            enemyToSpawn.SetActive(true);
        }
    }
}
