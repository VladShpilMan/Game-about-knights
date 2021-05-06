using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform attackPointRight;
    [SerializeField] protected Transform attackPointLeft;
    [SerializeField] protected LayerMask enemyLayers;

    [SerializeField] protected int attackDamage;
    [SerializeField] protected float attackRangeRight;
    [SerializeField] protected float attackRangeLeft;
    [SerializeField] protected float speedAnimCut;
    [SerializeField] protected float attackRate;

    protected SpriteRenderer sprite;
    protected float nextAttackTime;

    public virtual void Attack() { }

    void OnDrawGizmosSelected()
    { // the function draws the radius of the impact

        if (attackPointRight == null)
            return;
        if (attackPointLeft == null)
            return;

        Gizmos.DrawWireSphere(attackPointRight.position, attackRangeRight);

        Gizmos.DrawWireSphere(attackPointLeft.position, attackRangeLeft);
    }

}
