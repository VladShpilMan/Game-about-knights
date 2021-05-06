using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : AttackController
{
    private Transform character;
  
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        character = GameObject.FindGameObjectWithTag("Character").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        double chek = transform.position.x - character.position.x;
        if (chek < 0) chek *= -1;
        if (chek <= 1.2)
        {
            animator.SetBool("isMove", false);
            if (Time.time >= nextAttackTime)
            {
                if (Time.time >= nextAttackTime && character.GetComponent<Character>().inLive == true)
                {
                    Attack();
                    nextAttackTime = Time.time + 3F / attackRate;
                }
            }
        }

    }

    public override void Attack()
    {
        //animator.SetTrigger("Attack"); // Play an attack animation
        //animator.SetFloat("speedAnimCut", speedAnim);

        // Detect enemies in range in attack
        if (!sprite.flipX)
        {
            Collider2D[] hitCharacterRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRangeRight, enemyLayers);

            foreach (Collider2D character in hitCharacterRight)
            {
                if (character.GetComponent<Character>().isProtection == false)    //if the main character does not put up a shield
                    character.GetComponent<Character>().TakeDamage(attackDamage); // damage on the right side
            }
        }
        else
        {
            Collider2D[] hitCharacterLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRangeLeft, enemyLayers);

            foreach (Collider2D character in hitCharacterLeft)
            {
                if (character.GetComponent<Character>().isProtection == false)    //if the main character does not put up a shield
                    character.GetComponent<Character>().TakeDamage(attackDamage);   // damage on the left side
            }
        }
    }
}
