using UnityEngine;

public abstract class EnemyState : IState
{
    public IEnemy enemy;

    public EnemyState(IEnemy enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
}
