using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 platformPosition = new Vector3();
    public bool moveRight;
    public float leftBound;
    public float rightBound;
    private float xVelocity = 0.0f;
    public float smoothTime = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        platformPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= leftBound + 1)
        {
            moveRight = true;
        }
        if(transform.position.x >= rightBound - 1)
        {
            moveRight = false;
        }

        if(moveRight)
        {
            float distance = rightBound / smoothTime;
            platformPosition.x = Mathf.SmoothDamp(transform.position.x, transform.position.x + distance, ref xVelocity, smoothTime);
        }
        else 
        {
            float distance = leftBound / smoothTime;
            platformPosition.x = Mathf.SmoothDamp(transform.position.x, transform.position.x - distance, ref xVelocity, smoothTime);
        }
        transform.position = platformPosition;
    }
}
