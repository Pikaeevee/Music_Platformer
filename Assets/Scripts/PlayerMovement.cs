using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// PLAYER OBJECT MUST HAVE RIGIDBODY2D ATTACHED 
	public bool canMove = true;
	public float speed = 10.0f; 
	public float jumpSpeed = 8.0f;
    private bool isJumping = false;

    private bool isHighJumping = false; 

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
        if(canMove)
        {
            velocity = Input.GetAxis("Horizontal") * speed;
            rb.velocity = new Vector2(velocity, rb.velocity.y); 

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                // rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                isJumping = true;
            }
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
        // jumpSpeed *= 1.5f;
        rb.gravityScale *= 0.3f;
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
