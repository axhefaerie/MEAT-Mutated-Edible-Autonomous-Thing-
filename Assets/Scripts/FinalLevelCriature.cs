using UnityEngine;
using System.Collections;

public class FinalLevelCriature : MonoBehaviour
{
    public HUD hud;
    public MonoBehaviour playerMovement; // script thingMOvement
    public Animator playerAnimator;
    public int lvlNumber = 1;

    public float stopTime = 5f; // Time player stops

    private bool activated = false;

    // SOUND
    public AudioClip loopSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            GameManager.Instance.LevelCompleted(lvlNumber);
            activated = true;
            // Lanza la coroutine que reproduce el sonido después de un delay
            if (loopSound != null)
            {
                StartCoroutine(PlayLoopSoundAfterDelay(1.5f)); // 1 segundo de retraso, cámbialo
            }

            StartCoroutine(StopPlayer());
        }
    }

    private IEnumerator StopPlayer()
    {
        playerAnimator.SetBool("running", false);

        ThingMovement movement = playerMovement as ThingMovement;
        if (movement != null) { movement.StopRunningSound(); }

        playerMovement.enabled = false; // bloquear movimiento

        yield return new WaitForSeconds(2f);
        hud.ShowDialogue();        // show sandwich

        yield return new WaitForSeconds(stopTime);
        hud.HideDialogue();        // stop sandwich
        playerMovement.enabled = true;  // volver a moverse
    }

    private IEnumerator PlayLoopSoundAfterDelay(float delay)
    {
        // Espera antes de empezar el loop
        yield return new WaitForSeconds(delay);

        if (audioSource != null)
        {
            audioSource.clip = loopSound;
            audioSource.loop = true;
            audioSource.Play();

            // Detener después de stopTime segundos
            yield return new WaitForSeconds(stopTime);
            audioSource.Stop();
        }
    }
}
