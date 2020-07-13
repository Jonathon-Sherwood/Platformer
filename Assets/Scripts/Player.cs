using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float height = 1.1f;
    public int maxMultiJump;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Movement();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Movement()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed;

        rb.velocity = new Vector2(xMovement, rb.velocity.y);

        if(rb.velocity.x > 0)
        {
            sprite.flipX = false;
        } else if (rb.velocity.x < 0 )
        {
            sprite.flipX = true;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
    }

    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, (height / 2f) + 0.1f);

        return (hitInfo.collider != null);
    }

}
