using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float height = .5f;
    public int maxMultiJump;
    private int currentJumps;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public LayerMask floorLayerMask;
    private Animator anim;
    private bool playingSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
                Jump();
            }
        }

        if (!isGrounded())
        {
            AudioManager.instance.Stop("Movement");
            playingSound = false;
        }
    }

    private void Movement()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed;

        rb.velocity = new Vector2(xMovement, rb.velocity.y);

        if (rb.velocity.x != 0)
        {
            anim.Play("Moving");
            if(playingSound == false)
            {
                AudioManager.instance.Play("Movement");
                playingSound = true;
            }
        } else
        {
            anim.Play("Idle");
            if(playingSound == true)
            {
                AudioManager.instance.Stop("Movement");
                playingSound = false;
            }
        }

        if (rb.velocity.x > 0)
        {
            //sprite.flipX = false;
        } else if (rb.velocity.x < 0 )
        {
            //sprite.flipX = true;
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

}
