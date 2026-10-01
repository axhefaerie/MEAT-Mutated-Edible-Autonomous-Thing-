using UnityEngine;
using UnityEngine.Rendering;

public class AttackPlayerState : EnemyState
{
    // Cooldown
    private float attackCooldown = 2.5f;

    private float attackTimer;

    public AttackPlayerState(IEnemy enemy) : base(enemy) { }

    public override void Enter() 
    {
        attackTimer = attackCooldown;
        enemy.Animator.SetBool("isMoving", false);
        enemy.Animator.SetTrigger("attack");
    }

    
    public override void Exit() 
    {
        enemy.Animator.ResetTrigger("attack");
    }

    public override void Update() 
    {
        float dist = Vector2.Distance(enemy.Transform.position, enemy.Player.position);

        if (dist > 3.5)
        {
            enemy.SetState(new WalkingToPlayerState(enemy));
        }
    }

    public override void FixedUpdate()
    {
        attackTimer -= Time.fixedDeltaTime;

        if (attackTimer <= 0f)
        {
            //enemy.TryAttack();
            enemy.Animator.SetTrigger("attack");
            attackTimer = attackCooldown;
        }

    }

}
