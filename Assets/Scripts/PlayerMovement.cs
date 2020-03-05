using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// PLAYER OBJECT MUST HAVE RIGIDBODY2D ATTACHED 
	public float speed = 10.0f; 
	public float jumpSpeed = 8.0f;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.pm.playState == PlayerState.playing)
        {
            velocity = Input.GetAxis("Horizontal") * speed;
            // decrease how much player can move in the air 
            if (isJumping)
            {
                velocity *= 0.75f;
            }
            rb.velocity = new Vector2(velocity, rb.velocity.y); 

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                // rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                isJumping = true;
            }
        }
        // check for falling, multiply gravity 
        if (!isFalling && rb.velocity.y < 0)
        {
            isFalling = true;
            rb.gravityScale *= 2; 
        }
        else if (isFalling && rb.velocity.y >= 0)
        {
            isFalling = false;
            rb.gravityScale /= 2; 
        }
    }

    /*
    private void Jump()
    {
        //print("Attempting to Jump!");
        //Raycast to make sure we can jump
        RaycastHit2D results;
        LayerMask mask = LayerMask.GetMask("Ground");
        results = Physics2D.Raycast(transform.position, howCloseToJump.normalized, howCloseToJump.magnitude, mask);
        if (results.collider)
        {
            //print("We correctly collided!");
            //We hit something!
            // assuming ball is rigidbody
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Abs(rb.velocity.y) * bounceReturn);
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            //jumpcooldown = jumpWait;
        }
        else
        {
            //print("No collider hit!");
        }
    }
    */

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
        // jumpSpeed /= 1.5f;
        rb.gravityScale /= 0.3f;
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
