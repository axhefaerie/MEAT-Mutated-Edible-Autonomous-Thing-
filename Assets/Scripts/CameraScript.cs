using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject Thing;

    public float yOffset = 0.5f;
    public float minY = 0f;         // camera min height

    public float xOffset = 2f;
    public float minX = 0f;

    public float deadZoneUp = 1.5f;     // cuanto puede subir sin mover cámara
    public float deadZoneDown = 0.5f;   // cuanto puede bajar sin mover cámara

    public float smoothUp = 0.15f;
    public float smoothDown = 0.3f;

    private float yVelocity = 0f;

    void LateUpdate()
    {
        Vector3 position = transform.position;

        // -------- X --------
        float desiredX = Thing.transform.position.x + xOffset;
        position.x = Mathf.Max(desiredX, minX);

        // -------- Y --------
        float playerY = Thing.transform.position.y + yOffset;
        float targetY = position.y;

        if (playerY > position.y + deadZoneUp)
        {
            targetY = playerY - deadZoneUp;
        }
        else if (playerY < position.y - deadZoneDown)
        {
            targetY = playerY + deadZoneDown;
        }

        targetY = Mathf.Max(targetY, minY);

        float smooth = targetY > position.y ? smoothUp : smoothDown;

        position.y = Mathf.SmoothDamp(
            position.y,
            targetY,
            ref yVelocity,
            smooth
        );

        transform.position = position;
    }



}
