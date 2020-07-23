using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //Allows the designer to set the player's speed in the inspector.
    public float jumpForce; //Allows the designer to set the player's jump height in the inspector.
    private float height = .5f; //Used for the raycast that checks for the ground.
    public int maxMultiJump; //Allows the designer to set how often the player can jump mid air in the inspector.
    private int currentJumps; //Holds the current value of how many times the player has already jumped mid air.
    private Rigidbody2D rb; //Holds the player's rigidbody component.
    private SpriteRenderer sprite; //Holds the player's sprite renderer component.
    public LayerMask floorLayerMask; //Allows the designer to designate what counts as the floor for jumping.
    private Animator anim; //Holds the player's animator component.

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.player = this.gameObject;
        }
        currentJumps = maxMultiJump;
    }

    private void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        //Uses speed of direction rather than button press for movement.
        float xMovement = Input.GetAxis("Horizontal") * speed;

        rb.velocity = new Vector2(xMovement, rb.velocity.y);

        if (rb.velocity.x != 0)
        {
            anim.Play("Moving");
        }
        else
        {
            anim.Play("Idle");
        }

        if (rb.velocity.x > 0)
        {
            //sprite.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            //sprite.flipX = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Allows the player to grab collectables.
        if(collision.transform.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            GameManager.instance.currentCollectables++;
        }
    }


    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded()) //Resets jumps when the player lands.
            {
                currentJumps = maxMultiJump;
            }
            if (currentJumps > 0) //Applies a new velocity regardless of players previous velocity. Avoids massive jumps.
            {
                currentJumps--;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    //Uses a raycast to check for the floor. Used for double jumps and animations.
    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, (height / 2f) + 0.1f, floorLayerMask);

        return (hitInfo.collider != null);
    }

}