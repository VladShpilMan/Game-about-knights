using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    [SerializeField]
    private float speed = 3.0F;
    private float speedX;
    [SerializeField] private float jumpForce = 15.0F;
    [SerializeField] private float speedAnimHurt = 2F;
    [SerializeField] private float speedAnimProtection = 2F;


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
        animator.SetBool("isMove", false);       
        animator.SetBool("isGround", isGround);

        CheckGround();
        Run();
        Jump();
        Protection();
    }

    private void Run() {
        speedX = speed / 100;
        if (Input.GetAxisRaw("Horizontal") > 0 && !isProtection) {
            sprite.flipX = false;
            animator.SetBool("isMove", true);
            transform.Translate(speedX, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && !isProtection) {
            sprite.flipX = true;
            animator.SetBool("isMove", true);
            transform.Translate(-speedX, 0, 0);
        }

    }

    private void Jump() {
        if (Input.GetButtonDown("Jump") && isGround && isProtection == false)
        
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        

    }

    private void CheckGround() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        
        isGround = colliders.Length > 1;       
    }

    private void Protection() {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("speedAnimProtection", speedAnimProtection);
            isProtection = true;
            animator.SetBool("isProtection", true);
        }
        else
        {
            animator.SetBool("isProtection", false);
            isProtection = false;
        }
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
       // GetComponent<Collider2D>().enabled = false; // disable collider to gameObject
       GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false; // disable script for gameObject
        this.GetComponent<CharacterAttack>().enabled = false;
    }
}
