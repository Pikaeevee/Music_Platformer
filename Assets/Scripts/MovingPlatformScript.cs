using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 platformPosition = new Vector3();
    public bool movePositive;
    public float smoothTime = 0.0f;

    public float x1, y1;
    public float x2, y2;
    private Vector3 position1 = Vector3.zero;
    private Vector3 position2 = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        platformPosition = transform.position;
        position1 = new Vector3(x1, y1, 0.0f);
        position2 = new Vector3(x2, y2, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (ReachedPosition(position1) || ReachedPosition(position2))
        {
            movePositive = !movePositive;
        }

        if (movePositive)
        {
           platformPosition = Vector3.Lerp(platformPosition, position2, smoothTime * Time.deltaTime); 
        }
        else
        {
            platformPosition = Vector3.Lerp(platformPosition, position1, smoothTime * Time.deltaTime);
        }
        transform.position = platformPosition;
        
    }

    bool ReachedPosition(Vector3 position)
    {
        return (Mathf.Abs(transform.position.x - position.x) < 1.0f && Mathf.Abs(transform.position.y - position.y) < 1.0f);
    }
}
