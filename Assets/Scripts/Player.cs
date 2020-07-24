using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float height = .2f;
    public int maxMultiJump;
    private int currentJumps;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public LayerMask floorLayerMask;
    private Animator anim;
    private bool landedSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        GameManager.instance.player = this;
    }

    private void Start()
    {
        currentJumps = maxMultiJump;
    }

    private void Update()
    {
        Movement();
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded())
            {
                currentJumps = maxMultiJump;
            }
            if (currentJumps > 0)
            {
                AudioManager.instance.Play("Jump");
                Jump();
            }
        }
        LandingSound();
    }

    private void Movement()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed;

        rb.velocity = new Vector2(xMovement, rb.velocity.y);

        if (rb.velocity.x > 1f || -1f > rb.velocity.x)
        {
            anim.Play("Moving");
        } else
        {
            anim.Play("Idle");
        }
    }

    //Applies a new velocity regardless of players previous velocity. Avoids massive jumps.
    private void Jump()
    {
        currentJumps--;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, (height / 2f) + 0.1f, floorLayerMask);
        return (hitInfo.collider != null);
    }

    private void LandingSound()
    {
        //Only creates a landing sound once on impact, then resets afterward.
        if (isGrounded() && landedSound == false)
        {
            AudioManager.instance.Play("Land");
            landedSound = true;
        }
        if (!isGrounded())
        {
            landedSound = false;
        }
    }

}
