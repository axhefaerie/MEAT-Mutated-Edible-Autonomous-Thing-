using UnityEngine;
using UnityEngine.XR;

public class EnemyController : MonoBehaviour, IEnemy
{
   public int speed;
    public Animator animator;

    private EnemyState currentState;
    public Transform[] waypoints;

    public Transform player;

    private IAttackComponent attackComponent;
    private IMoveComponent moveComponent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        attackComponent = GetComponent<IAttackComponent>();
        moveComponent = GetComponent<IMoveComponent>();


        SetState(new SearchingWaypointState(this));
    }
   
    public void TryAttack()
    {
        attackComponent?.Attack();      
    }

    public void EndAttack()
    {
        if (attackComponent is MeleeAttackComponent melee)
            melee.StopAttack();
    }

    public void Move(Vector3 target) {
        moveComponent.Move(transform, target, speed);
    }

    void Update()
    {
        currentState?.Update();
    }

    void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public EnemyState GetState() => currentState;

    public void SetState(EnemyState newState)
    {
        currentState?.Exit();   // if current state not null
        currentState = newState;
        currentState.Enter();
    }

    // Se dan valores a los atributos de IEnemy
    public Transform Transform => transform;
    public Transform Player => player;
    public float Speed => speed;
    
    public Transform[] Waypoints => waypoints;
    public Animator Animator => animator;
   
    

}
