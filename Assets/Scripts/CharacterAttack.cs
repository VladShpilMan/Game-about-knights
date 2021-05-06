using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : AttackController
{

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1F / attackRate;
            }
        }
    }

    public override void Attack()
    {
        animator.SetTrigger("Attack"); // Play an attack animation
        animator.SetFloat("speedAnimCut", speedAnimCut);

        if (!sprite.flipX)
        {

            Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRangeRight, enemyLayers); // Detect enemies in range in attack right

            foreach (Collider2D enemy in hitEnemiesRight)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);     // damage on the right side
            }
        }
        else
        {

            Collider2D[] hitEnemiesLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRangeLeft, enemyLayers);  // Detect enemies in range in attack left

            foreach (Collider2D enemy in hitEnemiesLeft)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);     // damage on the left side
            }
        }
    }

}
