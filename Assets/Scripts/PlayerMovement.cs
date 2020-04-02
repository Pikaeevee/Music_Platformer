using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// PLAYER OBJECT MUST HAVE RIGIDBODY2D ATTACHED 
    public float speed = 10.0f; 
    public float jumpSpeed = 8.0f;
    private float dashForce = 100.0f; 
    public LayerMask groundLayer; 


    private bool isJumping = false;

    private bool isFalling = false;

    public bool canDash = false; 
    private bool isDashing = false; 


    private bool isHighJumping = false;


    // vars related to jumping 
    [SerializeField] private Vector2 howCloseToJump;
    public float bounceReturn = 0;

    private Rigidbody2D rb;

    private float velocity; 

    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = PlayerManager.pm.lastCheckpoint;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        groundLayer = LayerMask.GetMask("Ground"); 
    }

    // Update is called once per frame
    void Update()
    {
        // check if player isn't playing, halt any movement 
        if (PlayerManager.pm.GetPlayerState() != PlayerState.playing)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); 
        }
        else
        {
            velocity = Input.GetAxis("Horizontal") * speed;
            if(Input.GetAxis("Horizontal") < 0) {
              transform.localRotation = Quaternion.Euler(0, 180, 0);
              playerAnimator.SetBool("isWalking", true);
            }
            else if(Input.GetAxis("Horizontal") > 0) {
              transform.localRotation = Quaternion.Euler(0, 0, 0);
              playerAnimator.SetBool("isWalking", true);
            } else {
              playerAnimator.SetBool("isWalking", false);
            }
            
            // decrease how much player can move in the air 
            if (isJumping)
            {
                velocity *= 0.7f;
            }
            rb.velocity = new Vector2(velocity, rb.velocity.y); 

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                Jump();
                //rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                // rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                Dash();
            }
        }
        // check for falling, multiply gravity 
        
        if (!isFalling && !isGrounded())
        {
            isFalling = true;
            rb.gravityScale *= 2; 
        }
        else if (isFalling && isGrounded())
        {
            isFalling = false;
            rb.gravityScale /= 2;
        }
        
    }

    bool isGrounded()
    {
        // Debug.Log("checking if is grounded");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);
        if (hit.collider != null)
        {
            //Debug.Log("player is grounded");
            playerAnimator.SetTrigger("doneJump");
            playerAnimator.ResetTrigger("startJump");
            isJumping = false;
            return true; 
        }

        return false; 
    }

   
    private void Jump()
    {
        if (isGrounded())
        {
            //Debug.Log("jumping");
            isJumping = true;
            playerAnimator.SetTrigger("startJump");
            playerAnimator.ResetTrigger("doneJump");
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    public void HighJump()
    {
        if (isHighJumping)
        {
            Debug.Log("Already high jumping");
            return; 
        }
        Debug.Log("Activate High Jump");
        isHighJumping = true; 
        jumpSpeed *= 1.5f;
        //rb.gravityScale *= 0.3f;
    } 

    public void DeactivateHighJump() {
      // reset jumpspeed of player 
        jumpSpeed /= 1.5f;
        //rb.gravityScale /= 0.3f;
        isHighJumping = false; 
        Debug.Log("Deactivate High Jump");
    }

    public void Dash()
    {
        // only dash in air 
        if (!isGrounded() && !isDashing && canDash)
        {
            Debug.Log("Dashing");
            isDashing = true;
            float currg = rb.gravityScale; 
            rb.gravityScale = 0;  
            //rb.velocity = new Vector2(, 0);
            if (isFalling)
            {
                rb.AddRelativeForce(transform.right * dashForce * 5, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddRelativeForce(transform.right * dashForce, ForceMode2D.Impulse);
            }
            rb.gravityScale = currg;
            //rb.velocity = new Vector2(rb.velocity.x * dashForce, rb.velocity.y);
            StartCoroutine(DashCooldown());
        }
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Dash off cooldown");
        isDashing = false; 

    }

    public void AnotherAbility()
    {
        Debug.Log("Some other ability was done");
    } 

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Platform") 
        {
            if(isJumping) {
              playerAnimator.SetTrigger("doneJump");
              playerAnimator.ResetTrigger("startJump");
              isJumping = false;
            }
        }

        if(other.gameObject.tag == "Hazard")
        {
            if(isJumping) {
              playerAnimator.SetTrigger("doneJump");
              playerAnimator.ResetTrigger("startJump");
              isJumping = false;
            }
            PlayerManager.pm.LoseLife();
            transform.position = PlayerManager.pm.lastCheckpoint;
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.gameObject.transform;
        }

        if (other.gameObject.tag == "Checkpoint")
        {
            PlayerManager.pm.AddCheckpoint(other.gameObject);
            other.GetComponent<CheckpointController>().setActive();
            other.enabled = false;
        }    

        if (other.gameObject.tag == "Puzzle")
        {
            other.gameObject.GetComponent<PuzzleManager>().SetIsSolving(true);
        }

        if (other.gameObject.tag == "Exit")
        {
            PlayerManager.pm.NextLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }

        if (other.gameObject.tag == "Puzzle")
        {
            other.gameObject.GetComponent<PuzzleManager>().SetIsSolving(false);
        }
    }
}
