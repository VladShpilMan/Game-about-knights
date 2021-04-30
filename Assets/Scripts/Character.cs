﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {
    [SerializeField]
    private int maxHealth = 3;
    private int currentHealth;
    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;
    public float speedAnimHurt = 2F;
    public float speedAnimProtection = 2F;


    private bool isGround;
    public bool inLive;
    public bool isProtection = false;

    new private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private Animator animator;
    private Collider2D collider;


    private void Awake() {
        inLive = true;
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponentInChildren<Collider2D>();
    }

  

    private void FixedUpdate() {
        CheckGround();
    }

    private void Update() {

        if(Input.GetAxisRaw("Horizontal") > 0 && isProtection == false)
        {
            Run();
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0 && isProtection == false)
        {
            Run();
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);  // No Inertia
        }
        if(Input.GetButtonDown("Jump") && isGround && isProtection == false)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift)) {     
            animator.SetFloat("speedAnimProtection", speedAnimProtection);
            isProtection = true;
            animator.SetBool("isProtection", true);
        } else {
            animator.SetBool("isProtection", false);
            isProtection = false;
        }
        animator.SetBool("isGround", isGround);
        animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));
    }

    private void Run() {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;
      
    }

    private void Jump() {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
    }

    private void CheckGround() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        
        isGround = colliders.Length > 1;       
    }

    // the function deals damage to the object, if the object has no lives left, then it dies
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");  // play animation of taking damage
        animator.SetFloat("speedAnimHurt", speedAnimHurt);

        if (currentHealth <= 0) Die();
    }

    void Die() {
        Debug.Log("Character died!");

        animator.SetBool("isDead", true); // play animation of death
        animator.SetTrigger("death"); // play animation of death


        inLive = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // set the physical parameter to static, so that the gameObject does not fall into the ground
        GetComponent<Collider2D>().enabled = false; // disable collider to gameObject
        this.enabled = false; // disable script for gameObject
    }
}