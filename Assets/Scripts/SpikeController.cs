using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private bool playerNear = false;
    private GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is near and if player 
        if (playerNear)
        {
            if (player.GetComponent<PlayerController>().canControlSpikes)
            {
                // disable spikes 
                this.GetComponent<Renderer>().enabled = false;
                this.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(SpikeCooldown()); 
            }
        }
    }

    IEnumerator SpikeCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        {
            this.GetComponent<Renderer>().enabled = true;
            this.GetComponent<Collider2D>().enabled = true; 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // set player control to false in case it was true 
            other.gameObject.GetComponent<PlayerController>().canControlSpikes = false;
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerNear = false; 
        }
    }
}
