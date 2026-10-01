using UnityEngine;

public class Life : MonoBehaviour
{
    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(sound);

            GameManager.Instance.GainLife();
            Destroy(gameObject);
        }
    }
}


