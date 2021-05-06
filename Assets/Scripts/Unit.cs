using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
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
    }

    protected virtual void Die()
    {
        Debug.Log("Died!");
    }
}
