using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHighJump : MonoBehaviour
{
    public GameObject player;

    public float duration = 10.0f; 

    // Start is called before the first frame update
    void Start()
    {
        // double jumpspeed in player movement 
        player.GetComponent<PlayerMovement>().jumpSpeed *= 2;

        StartCoroutine(JumpBuffDuration());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator JumpBuffDuration()
    {
        yield return new WaitForSeconds(duration);

        // reset jumpspeed of player 
        player.GetComponent<PlayerMovement>().jumpSpeed *= 0.5f;
    }
}
