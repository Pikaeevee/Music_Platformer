using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 viewGoal;
    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;

    public float smoothTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerposition = player.transform.position;
        Vector3 cameraposition = transform.position;
        cameraposition.x = Mathf.Clamp(Mathf.SmoothDamp(cameraposition.x, playerposition.x, ref xVelocity, smoothTime), 0, 256);
        if(Mathf.Abs(playerposition.y - cameraposition.y) > 2.5f)
        {
            cameraposition.y = Mathf.SmoothDamp(cameraposition.y, playerposition.y, ref yVelocity, smoothTime);
        }
        transform.position = cameraposition;
    }
}
