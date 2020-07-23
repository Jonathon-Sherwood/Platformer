using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
<<<<<<< HEAD
    public float speed; //Allows the designer to set the player's speed in the inspector.
    public float jumpForce; //Allows the designer to set the player's jump height in the inspector.
    private float height = .5f; //Used for the raycast that checks for the ground.
    public int maxMultiJump; //Allows the designer to set how often the player can jump mid air in the inspector.
    private int currentJumps; //Holds the current value of how many times the player has already jumped mid air.
    private Rigidbody2D rb; //Holds the player's rigidbody component.
    private SpriteRenderer sprite; //Holds the player's sprite renderer component.
    public LayerMask floorLayerMask; //Allows the designer to designate what counts as the floor for jumping.
    private Animator anim; //Holds the player's animator component.
=======
    public float speed;
    public float jumpForce;
    public float height = 1.1f;
    public int maxMultiJump;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
>>>>>>> parent of 7790cf0... Double Jump/Animations

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
<<<<<<< HEAD
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.player = this.gameObject;
        }
        currentJumps = maxMultiJump;
=======
>>>>>>> parent of 7790cf0... Double Jump/Animations
    }

    private void Update()
    {
        Movement();
<<<<<<< HEAD
        Jump();
=======
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
>>>>>>> parent of 7790cf0... Double Jump/Animations
    }

    private void Movement()
    {
        //Uses speed of direction rather than button press for movement.
        float xMovement = Input.GetAxis("Horizontal") * speed;

        rb.velocity = new Vector2(xMovement, rb.velocity.y);

<<<<<<< HEAD
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
=======
        if(rb.velocity.x > 0)
        {
            sprite.flipX = false;
        } else if (rb.velocity.x < 0 )
>>>>>>> parent of 7790cf0... Double Jump/Animations
        {
            sprite.flipX = true;
        }
    }

<<<<<<< HEAD
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
=======
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
>>>>>>> parent of 7790cf0... Double Jump/Animations
    }

    //Uses a raycast to check for the floor. Used for double jumps and animations.
    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, (height / 2f) + 0.1f);

        return (hitInfo.collider != null);
    }

}