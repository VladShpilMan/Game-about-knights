using System.Collections;
using System.Collections.Generic;
using UnityEngine;


<<<<<<< HEAD
public class Character : Unit
{
    //private int currentHealth;
    private float speedX;
    // [SerializeField] private float speedAnimHurt = 2F;
=======
public class Character : Unit {
   // [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    //[SerializeField] private float speed = 3.0F;
    float speedX;
    [SerializeField] private float jumpForce = 15.0F;
    [SerializeField] private float speedAnimHurt = 2F;
>>>>>>> aa35c874db5769176954c7a93c7e5cf87eb21124
    [SerializeField] private float speedAnimProtection = 2F;


    private bool isGround;
    public bool inLive;
    public bool isProtection = false;

<<<<<<< HEAD
    //private Animator animator;
    private Collider2D collider;

    private void Awake()
    {
=======
    //private Rigidbody2D rigidbody;
   // private SpriteRenderer sprite;
    private Animator animator;
    private Collider2D collider;

    private void Awake() {
>>>>>>> aa35c874db5769176954c7a93c7e5cf87eb21124
        inLive = true;
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponentInChildren<Collider2D>();

    }

    //private void Start () {

    //}


    private void FixedUpdate()
    {
        animator.SetBool("isMove", false);
        animator.SetBool("isGround", isGround);


        Run();
        CheckGround();
        Protection();
    }

    private void Update()
    {

        Jump();
    }

    private void Run()
    {
        speedX = speed / 100;
        if (Input.GetAxisRaw("Horizontal") > 0 && !isProtection)
        {
            sprite.flipX = false;
            animator.SetBool("isMove", true);
            transform.Translate(speedX, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && !isProtection)
        {
            sprite.flipX = true;
            animator.SetBool("isMove", true);
            transform.Translate(-speedX, 0, 0);
        }

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround && !isProtection)
        {
            Debug.Log(jumpForce);
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGround = colliders.Length > 1;
    }

    private void Protection()
    {
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
<<<<<<< HEAD
    public override void TakeDamage(int damage)
    {

=======
    public override void TakeDamage(int damage) {
>>>>>>> aa35c874db5769176954c7a93c7e5cf87eb21124
        currentHealth -= damage;
        animator.SetTrigger("Hurt");  // play animation of taking damage
        animator.SetFloat("speedAnimHurt", speedAnimHurt);

        if (currentHealth <= 0) Die();
    }

    protected override void Die()
    {
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
