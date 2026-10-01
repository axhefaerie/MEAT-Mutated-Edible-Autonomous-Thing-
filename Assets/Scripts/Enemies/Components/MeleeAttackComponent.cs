using UnityEngine;

public class MeleeAttackComponent : MonoBehaviour, IAttackComponent
{
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] public int damage = 1;

    private TriggerComponent triggerComponent;

    private void Awake()
    {
        attackCollider.enabled = false;
        triggerComponent = GetComponent<TriggerComponent>();
    }

    public void Attack()
    {
        GetComponent<TriggerComponent>()?.ResetHit();
        triggerComponent.SetDamage(damage);
        attackCollider.enabled = true;
    }

    public void StopAttack()
    {
        attackCollider.enabled = false;
    }

}