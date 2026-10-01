using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour
{
    public ThingMovement player; // script thingMOvement

    public int boost = 1;
    public float boostDuration = 5f;

    private bool activated = false;

    private SpriteRenderer playerRenderer;
    private Color originalColor;

    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(sound);

            activated = true;           

            // Get player sprite
            playerRenderer = player.GetComponent<SpriteRenderer>();
            originalColor = playerRenderer.color;

            StartCoroutine(ApplyBoost());
        }
    }

    private IEnumerator ApplyBoost()
    {
        // Aply boost
        player.BulletDamage += boost;

        // Player turns blue
        playerRenderer.color = Color.green;

        // Hide booster (not destroying it yet)
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        // Wait
        yield return new WaitForSeconds(boostDuration);

        // Take boost off
        player.BulletDamage -= boost;

        // Restore color
        playerRenderer.color = originalColor;

        // DESTROY booster
        Destroy(gameObject);
    }
}
