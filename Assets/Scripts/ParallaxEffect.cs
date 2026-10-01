using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length;
    private float startPosX;
    private float startPosY;

    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        // Transform camera
        transform.position = new Vector3(startPosX + distance, startPosY, transform.position.z);

        if (temp > startPosX + length) startPosX += length;
        else if(temp < startPosX - length) startPosX -= length;
    }
}
