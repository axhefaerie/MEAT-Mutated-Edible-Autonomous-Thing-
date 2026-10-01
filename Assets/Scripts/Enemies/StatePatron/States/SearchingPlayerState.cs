using UnityEngine;

public class SearchingPlayerState : EnemyState
{
    // Constructor with values
    public SearchingPlayerState(IEnemy enemy) : base(enemy){}


    public override void Enter(){ }

    public override void Exit() { }

    public override void Update()
    {
        // Calculates distance between enemy and player
        float dist = Vector2.Distance(enemy.Transform.position, enemy.Player.position);
        // If the player is near the enemy, the enemy chases the player
        if (dist < 6) {
            enemy.SetState(new WalkingToPlayerState(enemy));
        }
        // If not, the enemy continues searching the next waypoint
        else
        {
            enemy.SetState(new WalkingToWaypointState(enemy));
        }
        
    }

    public override void FixedUpdate() { }
}
