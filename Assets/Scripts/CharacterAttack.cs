﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour {

    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPointRight;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Transform attackPointLeft;


    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRangee = 0.5f;
    [SerializeField] private float speedAnimCut = 2F;
    [SerializeField] private float attackRate = 2F;
    private float nextAttackTime = 0F;

    [SerializeField] private int attackDamage = 1;

    private SpriteRenderer sprite;

    void Start()
    {
       sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update() {
        if(Time.time >= nextAttackTime) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                Attack();
                nextAttackTime = Time.time + 1F / attackRate;
            }
        }      
    }

    void Attack() {
        animator.SetTrigger("Attack"); // Play an attack animation
        animator.SetFloat("speedAnimCut", speedAnimCut);

        if (!sprite.flipX)
        {
            
            Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers); // Detect enemies in range in attack right

            foreach (Collider2D enemy in hitEnemiesRight)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);     // damage on the right side
            }
        }
        else
        {

            Collider2D[] hitEnemiesLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRangee, enemyLayers);  // Detect enemies in range in attack left

            foreach (Collider2D enemy in hitEnemiesLeft)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);     // damage on the left side
            }
        }
    }

    void OnDrawGizmosSelected() { // the function draws the radius of the impact

        if (attackPointRight == null)
            return;
        if (attackPointLeft == null)
            return;

        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
       
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRangee);
    }
 }
