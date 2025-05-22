/* code for player movement and combat mechanics
 * 
 * Allows for player movement and actions with Unity's new input system
 * Has algorithm for timing the space between attacks (in blocks of 10ms),
 * used for timing combo attacks, combo attacks are recorded then checked to 
 * see if they match with a list of special move inputs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Time timer;
    public Rigidbody2D rb;
    public bool facingRight;
    [SerializeField] public bool isCrouching;
    [SerializeField] public GameObject GameManager;
    public Transform groundCheck;
    public Transform attackPoint; // attackPoint and attackRange variables can be adjusted for each character's attacks
    public Transform attackPointLow;
    public Transform attackPointHigh;
    public float attackRange = 0.5f; // maybe make different attackRange variables based on mid, low, high attack?
    public LayerMask enemyLayers;
    public LayerMask groundLayer;
    public ArrayList comboAttacks = new ArrayList();
    [SerializeField] public int[][] specialMoveList = new int[][] { // list of button inputs required for special move to be executed, should be changed for each character
        new int [] { 0, 1, 0 },
        new int [] { 1, 1, 1 },
        new int [] { 0, 2, 1 }
    };
    private int specialMoveIdentifier;
    // public Animator animator; // use for animation

    private float horizontal;
    private float startTime;
    private float elapsedTime;
    private bool isTiming = false;
    private float speed = 2f;
    [SerializeField] private float jumpingPower = 6f;
    [SerializeField] public bool slowed;
    [SerializeField] Collider2D standingCollider;
    [SerializeField] Collider2D crouchingCollider;
    private bool faceRight = true;
    private int attacknumdone = 0;
    [SerializeField] public bool isBlocking;

    void Start()
    {
        Debug.Log(CharacterSelectScript.charSelectP1);
        attacknumdone = 0;
        // Debug.Log(GameManagerScript.gameStarted);
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
        // Debug.Log("GameStarted: " + GameManagerScript.gameStarted);
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isTiming = true;
        Debug.Log("timer start");
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && GameManagerScript.gameStarted)
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

    public void Flip() // flips the player and sprite
    {
        faceRight = !faceRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        //Debug.Log("Is facing right: " + faceRight);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (GameManagerScript.gameStarted)
        {
            horizontal = context.ReadValue<Vector2>().x;
        }
        
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded() && GameManagerScript.gameStarted)
        {
            isCrouching = true;
            standingCollider.enabled = false;
            crouchingCollider.enabled = true;
        }
        else if (context.canceled)
        {
            isCrouching = false;
            standingCollider.enabled = true;
            crouchingCollider.enabled = false;
        }
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (context.started && GameManagerScript.gameStarted)
        {
            isBlocking = true;
            Debug.Log("block");
        }
        else if (context.canceled)
        {
            isBlocking = false;
            Debug.Log("no block");
        }
    }

    public void AttackLow(InputAction.CallbackContext context)
    {
        if (context.performed && GameManagerScript.gameStarted)
        {
            if (!IsGrounded())
            {
                Attack(3);
            }
            else if (isCrouching)
            {
                Attack(7);
            }
            else
            {
                Attack(0);
            }

        }

    }

    public void AttackMiddle(InputAction.CallbackContext context)
    {
        if (context.performed && GameManagerScript.gameStarted)
        {
            if (!IsGrounded())
            {
                Attack(4);
            }
            else if (isCrouching)
            {
                Attack(8);
            }
            else
            {
                Attack(1);
            }

        }

    }

    public void AttackHigh(InputAction.CallbackContext context)
    {
        if (context.performed && GameManagerScript.gameStarted)
        {
            if (!IsGrounded())
            {
                Attack(5);
            }
            else if (isCrouching)
            {
                Attack(9);
            }
            else
            {
                Attack(2);
            }

        }

    }

    public void AttackGrab(InputAction.CallbackContext context)
    {
        if (context.performed && GameManagerScript.gameStarted)
        {
            if (!IsGrounded())
            {
                return;
            }
            else
            {
                Attack(6);
            }
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
        attacknumdone++;
        Debug.Log("attack number done: " + attacknumdone);
        switch (type)
        {
            case 0:
                CheckCombo(0);

                //Debug.Log("low");
                // do an animation
                Collider2D[] hitEnemyLow = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers); // checks if attack hit opponent
                foreach (Collider2D enemy in hitEnemyLow)
                {
                    //Debug.Log("hit low");
                    //do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(25, false);
                }

                break;

            case 1:
                CheckCombo(1);

                //Debug.Log("mid");
                // animator.SetTrigger("Attack") do an animation
                Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); // checks if attack hit opponent
                foreach (Collider2D enemy in hitEnemy)
                {
                    //Debug.Log("hit middle");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(50, false);
                }

                break;

            case 2:
                CheckCombo(2);

                //Debug.Log("high");
                // do an animation
                Collider2D[] hitEnemyHigh = Physics2D.OverlapCircleAll(attackPointHigh.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyHigh)
                {
                    //Debug.Log("hit high");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(75, false);
                }
                break;

            case 3:
                CheckCombo(3);

                //Debug.Log("jumping low");
                Collider2D[] hitEnemyJumpLow = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyJumpLow)
                {
                    //Debug.Log("hit jumping low");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(35, false);

                }
                break;

            case 4:
                CheckCombo(4);

                //Debug.Log("jumping mid");
                Collider2D[] hitEnemyJumpMid = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyJumpMid)
                {
                    //Debug.Log("hit jumping mid");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(55, false);

                }
                break;

            case 5:
                CheckCombo(5);

                //Debug.Log("jumping high");
                Collider2D[] hitEnemyJumpHigh = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyJumpHigh)
                {
                    //Debug.Log("hit jumping high");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(80, false);

                }
                break;
            case 6:
                Debug.Log("grab");
                Collider2D[] hitEnemyGrab = Physics2D.OverlapCircleAll(attackPointHigh.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyGrab)
                {
                    Debug.Log("grab attack");
                    // damage
                    enemy.GetComponent<HealthScript>().UpdateHealth(100, true);

                }
                break;
            case 7:
                CheckCombo(7);

                Collider2D[] hitEnemyCrouchLow = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyCrouchLow)
                {
                    //Debug.Log("hit jumping high");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(20, false);

                }
                break;
            case 8:
                CheckCombo(8);

                Collider2D[] hitEnemyCrouchMid = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyCrouchMid)
                {
                    //Debug.Log("hit jumping high");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(30, false);

                }
                break;
            case 9:
                CheckCombo(9);

                Collider2D[] hitEnemyCrouchHigh = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemyCrouchHigh)
                {
                    //Debug.Log("hit jumping high");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(50, false);

                }
                break;
        }
    }

    public void SpecialAttack(int type)
    {
        switch (type)
        {
            case 0:
                Debug.Log("sp attack 0");
                Collider2D[] enemySPAttack0 = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in enemySPAttack0)
                {
                    //Debug.Log("hit jumping high");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(110, false);

                }

                break;
            case 1:
                Debug.Log("sp attack 1");
                Collider2D[] enemySPAttack1 = Physics2D.OverlapCircleAll(attackPointLow.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in enemySPAttack1)
                {
                    //Debug.Log("hit jumping high");
                    // do damage here
                    enemy.GetComponent<HealthScript>().UpdateHealth(115, false);

                }
                break;
        }
    }

    private bool CheckCombo(int lastAttack)
    {
        if (!isTiming)
        {
            StartTimer();
            comboAttacks.Add(lastAttack);
            foreach (int a in comboAttacks)
            {
                Debug.Log(a);
            }
            return false;
        }
        else
        {
            elapsedTime = ((Time.time - startTime) * 1000) / 10; // elapsed time is the time that has passed in chunks of 10 ms
            Debug.Log("Time elapsed: " + elapsedTime);
            if (elapsedTime < 35) // 350 ms to make it a little more forgiving for combos, can be fine-tuned depending on how difficult it ends up
            {
                comboAttacks.Add(lastAttack);
                Debug.Log("continue");
                elapsedTime = 0;
                //Debug.Log("reset");
                foreach (int a in comboAttacks)
                {
                    Debug.Log(a);
                }
                CheckSpecialMove(comboAttacks);
                return true;
            }
            else
            {
                Debug.Log("End");
                isTiming = false;
                elapsedTime = 0;
                comboAttacks.Clear();
                Debug.Log("Cleared");
                //Debug.Log("reset");
                return false;
            }
        }
    }

    public void CheckSpecialMove(ArrayList a) // we should probably clean this up later if possible
    {
        int check = 0;
        bool matches = true;
        if (a.Count >= 3)
        {
            //Debug.Log("y");
            foreach (int[] specialMove in specialMoveList)
            {
                //Debug.Log("a");
                for (int i = 0; i < specialMove.Length; i++)
                {
                    //Debug.Log("b");
                    if (specialMove[i] != (int)a[i])
                    {
                        //Debug.Log("f");
                        matches = false;
                    }
                    else
                    {
                        //Debug.Log("t");
                    }
                }
                if (matches == true)
                {
                    Debug.Log("match");
                    specialMoveIdentifier = check;
                    Debug.Log("specialMoveIdentifier: " + specialMoveIdentifier);
                    SpecialAttack(specialMoveIdentifier);
                }
                check++;
                matches = true;
            }
        }


    }

    private void OnDrawGizmosSelected() // gives visual representation of each attackPoint's hitbox, iirc shouldn't be visible in game view
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


  
