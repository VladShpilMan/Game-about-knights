using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    [SerializeField] private int speed = 4;
    private float speedAtMoment;
    [SerializeField] private int positionOfPatrol = 5;
    [SerializeField] private int attackDamage = 1;


    [SerializeField] private Transform point;
    private Transform character;
    private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D rigidbody;
    [SerializeField] private Transform attackPointRight;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Transform attackPointLeft;

    private bool isAttack;
    private bool movingRight;
    private bool chill = false;
    private bool angry = false;
    private bool goBack = false;

    [SerializeField] private float stoppingDistance;
    [SerializeField] private float speedAnimHurt = 2F;
    [SerializeField] private float speedAnimCut = 2f;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRangee = 0.5f;
    [SerializeField] private float speedAnim = 2F;
    [SerializeField] private float attackRate = 2F;
    [SerializeField] private float periodAttackRate = 3F;
    private float nextAttackTime = 0F;
    private float periodAttack = 0f;

    void Start()
    {
        speedAtMoment = (float)speed;
        rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        sprite = GetComponentInChildren<SpriteRenderer>();
        character = GameObject.FindGameObjectWithTag("Character").transform;
    }

    void FixedUpdate()
    { // i check the states of the object from the function

        /*if the distance between the gameObject and the patrol center Point is less 
            than the allowed patrol length and the gameObject is not in a state of "angry", then enable state "chill"*/
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false) chill = true;


        /*if the distance between the gameObject and the MainCharacter is greater 
            than the maximum visibility of the game object, then the state changes to "goBack"*/
        if (Vector2.Distance(transform.position, character.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }


        /*if the distance between the gameObject and the MainCharacter is less 
            than the maximum visibility of the game object, then the state changes to "angry"*/
        if (Vector2.Distance(transform.position, character.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;
        }


        // set an action based on a state parameter
        if (chill == true) Chill();

        else if (angry == true) Angry();

        else if (goBack == true) GoBack();

        if (rigidbody.velocity.x == 0)
            animator.SetFloat("Speed", 0); // if the game object is standing then the speed of the running animation = 0
        else
            animator.SetFloat("Speed", speedAtMoment);
    }

    void Chill()
    {
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            animator.SetBool("isMove", true);
            sprite.flipX = false;
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            animator.SetBool("isMove", true);
            sprite.flipX = true;
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void Angry()
    {
        double chek = transform.position.x - character.position.x;
        if (chek < 0) chek *= -1;

        // the gameObject approaches the MainCharacter if the distance between them is greater than the parameter
        if (chek >= 1.2)
        {
            if (transform.position.x - character.position.x > 0 && movingRight == true)
            {  // flip the sprite horizontally
                sprite.flipX = true;
            }

            if (transform.position.x - character.position.x < 0 && movingRight == false)
            { //flip the sprite horizontally
                sprite.flipX = false;
            }


            speedAtMoment = 2.8f; // the gameObject increases its speed in case of aggression

            if (movingRight)
            {           
                transform.position = Vector2.MoveTowards(transform.position, character.position, speedAtMoment * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, character.position, speedAtMoment * Time.deltaTime);
            }


        }


        // if the gameObject approached the MainCharacter at the maximum allowed distance, then the game object can make a blow
        if (chek <= 1.2)
        {
            animator.SetBool("isMove", false);
            isAttack = true;
            if (Time.time >= nextAttackTime)
            {
                if (Time.time >= nextAttackTime && character.GetComponent<Character>().inLive == true)
                {
                    Attack();
                    nextAttackTime = Time.time + 3F / attackRate;
                }
            }
        }
        if (character.GetComponent<Character>().inLive == false) animator.SetBool("isMove", false);

    }

    void GoBack()
    {
        if (point.position.x - transform.position.x > 0)
        {
            sprite.flipX = false;
            transform.position = Vector2.MoveTowards(transform.position, point.position, speedAtMoment * Time.deltaTime);
        }
        else
        {
            sprite.flipX = true;
            transform.position = Vector2.MoveTowards(transform.position, point.position, speedAtMoment * Time.deltaTime);
        }
        animator.SetBool("isMove", true);
    }

    void Attack()
    {
        animator.SetTrigger("Attack"); // Play an attack animation
        animator.SetFloat("speedAnimCut", speedAnim);

        // Detect enemies in range in attack
        if (!sprite.flipX)
        {
            Collider2D[] hitCharacterRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);

            foreach (Collider2D character in hitCharacterRight)
            {
                if (character.GetComponent<Character>().isProtection == false)    //if the main character does not put up a shield
                    character.GetComponent<Character>().TakeDamage(attackDamage); // damage on the right side
            }
        }
        else
        {
            Collider2D[] hitCharacterLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRangee, enemyLayers);
            foreach (Collider2D character in hitCharacterLeft)
            {
                if (character.GetComponent<Character>().isProtection == false)    //if the main character does not put up a shield
                    character.GetComponent<Character>().TakeDamage(attackDamage);   // damage on the left side
            }
        }
    }

    void OnDrawGizmosSelected()
    { // the function draws the radius of the impact

        if (attackPointRight == null)
            return;
        if (attackPointLeft == null)
            return;

        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);

        Gizmos.DrawWireSphere(attackPointLeft.position, attackRangee);
    }


    // the function deals damage to the object, if the object has no lives left, then it dies
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt"); // play animation of taking damage
        animator.SetFloat("speedAnimHurt", speedAnimHurt);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true); // play animation of death

        //GetComponent<Collider2D>().enabled = false; // disable collider to gameObject
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // set the physical parameter to static, so that the gameObject does not fall into the ground
        this.enabled = false; // disable script for gameObject
    }
}