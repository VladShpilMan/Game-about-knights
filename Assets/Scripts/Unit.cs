using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float speed;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rigidbody;

    public virtual void TakeDamage(int damage)
    {
      Die();
    }

    protected virtual void Die()
    {
        Debug.Log("Character died!");

    }
}
