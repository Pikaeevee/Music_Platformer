using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 platformPosition = new Vector3();
    public bool movePositive;
    public bool vertical;
    public float lowerBound;
    public float upperBound;
    private float xVelocity = 0.0f;
    public float smoothTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        platformPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(vertical)
        {
            VerticalMove();
        }
        else
        {
            HorizontalMove();
        }
        
    }

    void VerticalMove()
    {
        if(transform.position.y <= lowerBound + 1)
        {
            movePositive = true;
        }
        if(transform.position.y >= upperBound - 1)
        {
            movePositive = false;
        }

        if(movePositive)
        {
            platformPosition.y = Mathf.SmoothDamp(transform.position.y, transform.position.y + 10, ref xVelocity, smoothTime);
        }
        else 
        {
            platformPosition.y = Mathf.SmoothDamp(transform.position.y, transform.position.y - 10, ref xVelocity, smoothTime);
        }
        transform.position = platformPosition;
    }

    void HorizontalMove()
    {
        if(transform.position.x <= lowerBound + 1)
        {
            movePositive = true;
        }
        if(transform.position.x >= upperBound - 1)
        {
            movePositive = false;
        }

        if(movePositive)
        {
            platformPosition.x = Mathf.SmoothDamp(transform.position.x, transform.position.x + 10, ref xVelocity, smoothTime);
        }
        else 
        {
            platformPosition.x = Mathf.SmoothDamp(transform.position.x, transform.position.x - 10, ref xVelocity, smoothTime);
        }
        transform.position = platformPosition;
    }
}
