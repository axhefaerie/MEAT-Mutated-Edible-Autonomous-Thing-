using UnityEngine;

public interface IMoveComponent
{
    void Move(Transform move, Vector3 target, float speed);
}
