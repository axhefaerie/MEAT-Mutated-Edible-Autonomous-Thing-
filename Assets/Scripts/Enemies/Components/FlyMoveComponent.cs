using UnityEngine;

public class FlyMoveComponent : MonoBehaviour, IMoveComponent
{
    public AudioClip flySound;
    public float soundDistance = 13f;

    private AudioSource audioSource;
    private bool isPlaying = false;
    private Transform player;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = flySound;
        audioSource.loop = true;
        audioSource.volume = 0.7f;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Move(Transform move, Vector3 target, float speed)
    {
        float distance = Vector2.Distance(move.position, player.position);
        // Solo suena cuando el jugador está relativamente cerca
        if (distance < soundDistance)
        {
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {
                audioSource.Stop();
                isPlaying = false;
            }
        }

        // Enemy stays away from player 
        float heightOffset = 1; // + floating effect
        Vector3 targetPos = target;
        targetPos.y += heightOffset;

        // Enemies chase the player/waypoint on the x and y edge as they fly
        move.position = Vector3.MoveTowards(
            move.position,
            targetPos,
            speed * Time.deltaTime);

        // Horizontal flip
        Vector3 scale = move.localScale;
        float dirX = target.x - move.position.x;
        if (dirX > 0) scale.x = Mathf.Abs(scale.x);
        else if (dirX < 0) scale.x = -Mathf.Abs(scale.x);
        move.localScale = scale;
    }

    private void OnDisable()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}
