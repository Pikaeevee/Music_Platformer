using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// PLAYER OBJECT MUST HAVE RIGIDBODY2D ATTACHED 
    public float speed = 10.0f; 
    public float jumpSpeed = 8.0f;
    public LayerMask groundLayer; 


    private bool isJumping = false;

    private bool isFalling = false; 


    private bool isHighJumping = false;

    // vars related to jumping 
    [SerializeField] private Vector2 howCloseToJump;
    public float bounceReturn = 0;

    private Rigidbody2D rb;

    private float velocity; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground"); 
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is fighting, halt any movement 
        if (PlayerManager.pm.playState == PlayerState.fighting)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); 
        }
        if (PlayerManager.pm.playState == PlayerState.playing)
        {
            velocity = Input.GetAxis("Horizontal") * speed;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, groundLayer);
        if (hit.collider != null)
        {
            // Debug.Log("player is grounded");
            return true; 
        }

        return false; 
    }

   
    private void Jump()
    {
        if (isGrounded())
        {
            Debug.Log("jumping");
            isJumping = true;
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
        StartCoroutine(JumpBuffDuration());
    }   

    public void AnotherAbility()
    {
        Debug.Log("Some other ability was done");
    } 

    private IEnumerator JumpBuffDuration()
    {
        yield return new WaitForSeconds(10.0f);

        // reset jumpspeed of player 
        jumpSpeed /= 1.5f;
        //rb.gravityScale /= 0.3f;
        isHighJumping = false; 
        Debug.Log("Deactivate High Jump");
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Platform") 
        {
            isJumping = false;
        }    
    }
}
