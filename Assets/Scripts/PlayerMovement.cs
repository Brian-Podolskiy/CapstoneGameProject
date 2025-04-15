using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Time timer;
    public Rigidbody2D rb;
    public bool facingRight;
    public Transform groundCheck;
    public Transform attackPoint;
    public Transform attackPointLow;
    public Transform attackPointHigh;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask groundLayer;
    // public Animator animator; use for animation


    private float horizontal;
    private float startTime;
    private float elapsedTime;
    private bool isTiming = false;
    private float speed = 2f;
    private float jumpingPower = 4f;
    private bool faceRight = true;
    private int lastAttackDone;

    void Start()
    {
        
    }

    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!faceRight && horizontal > 0f)
        {
            Flip();
        }
        else if (faceRight && horizontal < 0f)
        {
            Flip();
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isTiming = true;
        Debug.Log("timer start");
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        faceRight = !faceRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        Debug.Log("Is facing right: " + faceRight);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        //Attack(0);
    }

    public void AttackLow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack(0);
        }
        
    }

    public void AttackMiddle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack(1);
        }
        
    }

    public void AttackHigh(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack(2);
        }

    }


    /* Idea for how to do button combos:
     * player presses attack button
     * timer starts
     * if player presses attack within certain number of milliseconds, combo continues
     * if timer elapses certain point, combo is aborted
     */

    public void Attack(int type) 
    {
        //Debug.Log("Attack");
        switch (type)
        {
            case 0:
                if (!isTiming)
                {
                    StartTimer();
                }

                else
                {
                    elapsedTime = Time.time - startTime;
                    Debug.Log(elapsedTime);
                    if (elapsedTime < 5)
                    {
                        Debug.Log("continue");
                        elapsedTime = 0;
                    }
                    else
                    {
                        Debug.Log("End");
                        elapsedTime = 0;
                    }
                }

                Debug.Log("low");
                // do an animation
                Collider2D[] hitEnemyLow = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyLow)
                {
                    Debug.Log("hit low");
                    //do damage here
                }

                // if animation connects - do damage to enemy
                lastAttackDone = 0;
                break;
            case 1:
                Debug.Log("mid");
                // animator.SetTrigger("Attack") do an animation
                Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemy)
                {
                    Debug.Log("hit middle");
                    // do damage here
                }

                // if animation connects - do damage to enemy
                lastAttackDone = 1;
                break;
            case 2:
                Debug.Log("high");
                // do an animation
                Collider2D[] hitEnemyHigh = Physics2D.OverlapCircleAll(attackPointHigh.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyHigh)
                {
                    Debug.Log("hit high");
                    // do damage here
                }
                lastAttackDone = 2;
                break;

        }    
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        if (attackPointLow == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPointLow.position, attackRange);

        if (attackPointHigh == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPointHigh.position, attackRange);
    }
}
