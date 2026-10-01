using UnityEngine;

public class FlyingEffect : MonoBehaviour
{
    public FlyMoveComponent moveComponent;

    public float amplitude = 0.2f;   // Qué tanto sube y baja
    public float frequency = 2f;     // Qué tan rápido flota
    public bool isMoving;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (!isMoving)
        {
            float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
            //moveComponent.floatingOffsetY = yOffset;
        }
        else
        {
            //ns
        }
    }
}
