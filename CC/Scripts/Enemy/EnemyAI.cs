using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform playerTransform;
    Rigidbody2D rigidbody2D;
    Collider2D collider2D;
    private bool grounded;
    Vector3 moveDirection;
    float gravityScale = 1f;
    float mass = 1f;
    float JumpForce = 200f;
    float MaxJumpingHeight = 1.5f;
    int attackCoolDown;
    int attackDamage = 10;
    IEnumerator moveCoroutine;
    IEnumerator attackCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("main_character").transform;
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        collider2D = transform.GetComponent<Collider2D>();
        JumpForce = Mathf.Sqrt((MaxJumpingHeight * (2f * gravityScale * 9.8f) * mass));
        grounded = false;
        moveCoroutine = null;
        attackCoroutine = AttackCoolDown();
        StartCoroutine(attackCoroutine);
        attackCoolDown = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) < 5)
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
            FindPlayer();
        }
        else
        {
            if (moveCoroutine == null)
            {
                moveCoroutine = Wandering();
                StartCoroutine(moveCoroutine);
            }
        }
        Move();
    }

    private bool isGround()
    {
        float extra_height = 0.1f;
        RaycastHit2D HitGround = Physics2D.Raycast(collider2D.bounds.center, Vector2.down, collider2D.bounds.extents.y + extra_height, (1 << 6));

        if (HitGround.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private bool HitWall()
    {
        RaycastHit2D Hit = Physics2D.Raycast(collider2D.bounds.center, moveDirection, collider2D.bounds.extents.x + 0.1f, (1 << 6));
        if (Hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CanJumpOver()
    {
        RaycastHit2D Hit = Physics2D.Raycast(collider2D.bounds.center + MaxJumpingHeight * Vector3.up, moveDirection, collider2D.bounds.extents.x + 0.1f, (1 << 6));
        if (Hit.collider == null)
        {
            Debug.Log("can jump over");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FindPlayer()
    {
        if (playerTransform.position.x > transform.position.x)
        {
            moveDirection = Vector3.right;
        }
        else
        {
            moveDirection = Vector3.left;
        }
    }

    private IEnumerator Wandering()
    {
        float r;
        while (true)
        {
            r = Random.Range(0, 3);
            Debug.Log(r);
            if (r == 0)
            {
                moveDirection = Vector3.right;
            }
            else if(r==1)
            {
                moveDirection = Vector3.left;
            }
            else
            {
                moveDirection = new Vector3(0, 0, 0);
            }
            yield return new WaitForSeconds(2);
        }
    }
    private IEnumerator AttackCoolDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            attackCoolDown = Mathf.Max(0, attackCoolDown-1);
        }
    }
    private void Move()
    {
        if (HitWall())
        {
            Debug.Log("hit wall");
            if (!isGround())
            {
                return;
            }
            if (CanJumpOver())
            {
                rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
                rigidbody2D.AddForce(new Vector3(0, JumpForce, 0), ForceMode2D.Impulse);
            }
            else
            {
                moveDirection = new Vector3(0, 0, 0);
            }
        }
        transform.Translate(moveDirection * Time.deltaTime, Camera.main.transform);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != "main_character")
        {
            return;
        }

        if (attackCoolDown == 0)
        {
            //Debug.Log("attack!");
            collision.gameObject.GetComponent<status>().takeDamage(attackDamage);
            attackCoolDown = 2;
        }
    }
}