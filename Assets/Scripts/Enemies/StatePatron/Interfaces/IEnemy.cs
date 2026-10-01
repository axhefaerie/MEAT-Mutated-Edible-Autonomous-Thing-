using UnityEngine;

public interface IEnemy
{
    // Variables needed for each enemy (EnemyController asignes the correct value)
    Transform Transform { get; }
    Transform Player { get; }
    float Speed { get; }
    Transform[] Waypoints { get; }
    public Animator Animator { get; }

    void Move(Vector3 target);
    void TryAttack();

    // Setter and Getter
    EnemyState GetState();
    void SetState(EnemyState newState);
}
