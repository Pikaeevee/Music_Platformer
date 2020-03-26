using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{

    //public string direction;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.pm.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().canControlSpikes)
        {
            HideSpikes();
        }
    }

    void HideSpikes()
    {
        // TODO: Animation for hiding spikes
        // switch (direction)
        // {
        //     case "down":

        //         break;
        //     case "up":

        //         break;
        //     case "right":

        //         break;
        //     case "left":

        //         break;
        //     default:
        //         Debug.Log("Error: unfilled direction");
        //         break;
        // }
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(SpikeCooldown());
    }

    IEnumerator SpikeCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        this.GetComponent<Renderer>().enabled = true;
        this.GetComponent<Collider2D>().enabled = true; 
    }
}
