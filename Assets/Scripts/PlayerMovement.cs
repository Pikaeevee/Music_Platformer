using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// PLAYER OBJECT MUST HAVE RIGIDBODY2D ATTACHED 
	
	public float speed = 10.0f; 
	public float jumpSpeed = 8.0f;

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
        velocity = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(velocity, rb.velocity.y); 

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }
}
