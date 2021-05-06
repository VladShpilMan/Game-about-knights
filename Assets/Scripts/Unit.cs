using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
<<<<<<< HEAD
    protected int currentHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
    protected Animator animator;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rigidbody;
    [SerializeField] protected float speedAnimHurt;

    //public virtual void Awake()
    //{
    //    currentHealth = maxHealth;
    //    animator = GetComponent<Animator>();
    //    sprite = GetComponentInChildren<SpriteRenderer>();
    //}

    public virtual void TakeDamage(int damage)
    {
        Die();
=======
    [SerializeField] protected float speed;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rigidbody;

    public virtual void TakeDamage(int damage)
    {
      Die();
>>>>>>> aa35c874db5769176954c7a93c7e5cf87eb21124
    }

    protected virtual void Die()
    {
<<<<<<< HEAD
        Debug.Log("Died!");
=======
        Debug.Log("Character died!");

>>>>>>> aa35c874db5769176954c7a93c7e5cf87eb21124
    }
}
