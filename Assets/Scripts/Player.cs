using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //Adjustable player speed in inspector.
    public float jumpForce; //Adjustable jump height in inspector.
    private float height = .2f; //Sets the ray height for groundchecking.
    public int maxMultiJump; //Adjustabel amount of extra jumps.
    private int currentJumps; //Holds the value of the current jumps the player has done before hitting the ground.
    private Rigidbody2D rb; //Holds the value of the player's rigibody.
    private SpriteRenderer sprite; //Holds the value of the player's sprite renderer.
    public LayerMask floorLayerMask; //Allows the raycast to specify what colliders it hits.
    private Animator anim; //Holds the value of the player's animator.
    private bool landedSound; //Stops the landing sound effect from going off more than once per landing.
    public float dashSpeed; //Adjustable dash distance for player.
    private float dashTime; //Holds the value of how long the player has already tried to dash.
    public float dashMaxTime; //Adjustable amount of time the player can dash.
    private bool hasWings; //Only allows the player to dash if they have wings.
    private bool wooshSound = false; //Used to not allow the woosh sound effect to repeat.
    public GameObject wings; //Calls the wings attached the player for displaying or not.

    private void Awake()
    {
        //Sets each variable to the parts attached to the player.
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        GameManager.instance.player = this;
    }

    private void Start()
    {
        currentJumps = maxMultiJump; //Sets the initial jumps on start.
    }

    private void Update()
    {
        Movement();
        if (isGrounded()) //Resets jumps whenver touching the ground.
        {
            currentJumps = maxMultiJump;
        }

        //Allows for multiple jumps that resets when hitting the ground.
        if (Input.GetButtonDown("Jump"))
        {  
            if (currentJumps > 0)
            {
                AudioManager.instance.Play("Jump");
                Jump();
            }
        }
        LandingSound();

        //Once the dash runs out, the wings expire and the player can no longer dash.
        if(dashTime <= 0)
        {
            hasWings = false;
            wings.SetActive(false);
            wooshSound = false;
        }
    }

    //Uses the Unity derived axis for movement and animations.
    private void Movement()
    {

        float xMovement = Input.GetAxis("Horizontal") * speed;         //Sets a float to the value added by Unity's input for horizontal.

        rb.velocity = new Vector2(xMovement, rb.velocity.y);           //The velocity is always tied to the input from Unity.

        //If the player is moving, the animation is switched to moving, otherwise it is idle.
        if (rb.velocity.x > 1f || -1f > rb.velocity.x)
        {
            anim.Play("Moving");
        } else
        {
            anim.Play("Idle");
        }

        //Shoots the player forward based on the direction they are moving.
        if (Input.GetKey(KeyCode.LeftShift) && dashTime > 0 && hasWings)
        {
            rb.velocity = new Vector2(xMovement * dashSpeed, rb.velocity.y);
            dashTime -= Time.deltaTime;

            //Plays a sound only once per button press.
            if (wooshSound == false)
            {
                AudioManager.instance.Play("Whoosh");
                wooshSound = true;
            }
        }
    }

    //Applies a new velocity regardless of players previous velocity. Avoids massive jumps.
    private void Jump()
    {
        currentJumps--;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    //Shoots a raycast beneath the player to check for the floor.
    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, (height / 2f) + 0.1f, floorLayerMask);
        return (hitInfo.collider != null);
    }

    //Only creates a landing sound once on impact, then resets afterward.
    private void LandingSound()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Turns on the ability to dash mid air on collision with a wing object.
        if (collision.CompareTag("Wings"))
        {
            hasWings = true;
            wings.SetActive(true);
            Destroy(collision.gameObject);
            dashTime = dashMaxTime;
            AudioManager.instance.Play("Choir");
        }
    }

}
