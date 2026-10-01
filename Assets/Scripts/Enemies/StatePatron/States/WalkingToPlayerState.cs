using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WalkingToPlayerState : EnemyState
{
    // Constructor with values
    public WalkingToPlayerState(IEnemy enemy) : base(enemy){}

    public override void Enter(){   }

    public override void Exit() { }

    public override void Update() {
        // If the player distances from the enemy, the enemy goes back to searching and going to the waypoints asigned
        float dist = Vector2.Distance(enemy.Transform.position, enemy.Player.position);
        
        // If distance is less than 7, the enemy attacks the player
        if (dist < 7f && !(enemy.GetState() is AttackPlayerState))
        {
            enemy.SetState(new AttackPlayerState(enemy));
        }
        // If distance is more than 13, the enemy continues searching waypoints
        if (dist > 13f) {
            enemy.SetState(new SearchingWaypointState(enemy));
        }
    }

    public override void FixedUpdate() {
        enemy.Animator.SetBool("isMoving", true);
        // Moves to player
        enemy.Move(enemy.Player.position);
    }
}
