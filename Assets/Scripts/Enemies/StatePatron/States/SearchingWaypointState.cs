using UnityEngine;

public class SearchingWaypointState : EnemyState
{
    // Constructor with values
    public SearchingWaypointState(IEnemy enemy) : base(enemy){}


    public override void Enter(){ }

    public override void Exit() { }

    public override void Update()
    {
        // Walks to the waypoint
        enemy.SetState(new WalkingToWaypointState(enemy));
    }

    public override void FixedUpdate() { }
}
