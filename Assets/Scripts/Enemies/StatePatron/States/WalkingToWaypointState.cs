using Unity.VisualScripting;
using UnityEngine;

public class WalkingToWaypointState : EnemyState
{
    // Waypoint values
    private int targetIndex;
    private Transform target;

    // Waiting time
    private float waitTime = 3f;    // seconds to wait in each waypoint
    private float waitTimer = 0f;   // seconds waited in each waypoint
    private bool isWaiting = false;

    // Constructor with values
    public WalkingToWaypointState(IEnemy enemy) : base(enemy){}

    public override void Enter()
    {
        // Asignes the first waypoint
        targetIndex = 0;
        target = enemy.Waypoints[targetIndex];

        isWaiting = false;
        waitTimer = 0f;
    }

    public override void Exit() { }

    public override void Update()
    {
        // Checks if player is near the enemy
        if (Vector2.Distance(enemy.Transform.position, enemy.Player.position) < 8f)
        {
            enemy.SetState(new SearchingPlayerState(enemy));
        }
    }

    public override void FixedUpdate()
    {
        float dist = Vector2.Distance(enemy.Transform.position, enemy.Player.position);
        // If player is close it attacks
        if (dist < 3.5f && !(enemy.GetState() is AttackPlayerState))
        {
            enemy.SetState(new AttackPlayerState(enemy));
        }
    
        // If the enemy has arrived to the actual waypoint, it waits in the waypoint
        if (isWaiting)
        {
            enemy.Animator.SetBool("isMoving", false);
            waitTimer -= Time.fixedDeltaTime;
            // If the waiting time has reached 0, it changes to the other waypoint
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                // Alternates waypoint as there are only 2 waipoints targeted as 0 and 1
                targetIndex = 1 - targetIndex;
                target = enemy.Waypoints[targetIndex];
                enemy.Animator.SetBool("isMoving", true);
            }
            return; // doesnt move while it waits
        }
        enemy.Move(target.position);

        // If the enemy arrives to waypoint
        if (Vector2.Distance(enemy.Transform.position, target.position) < 0.1f)
        {
            isWaiting = true;
            waitTimer = waitTime;
        }

    }
}
