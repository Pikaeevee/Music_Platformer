using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    Animator bossAnimator;
    EnemyController bossController;
    private bool bossPlaying = false; 

    // Start is called before the first frame update
    void Start()
    {
        bossAnimator = gameObject.GetComponent<Animator>();
        bossController = gameObject.GetComponent<EnemyController>();

        bossAnimator.SetBool("isPlaying", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossController.donePlaying && !bossPlaying)
        {
            Debug.Log("boss playing"); 
            bossPlaying = true;
            bossAnimator.SetBool("isPlaying", true);
            bossAnimator.SetBool("isIdle", false);
        }
        else if (bossController.donePlaying && bossPlaying)
        {
            Debug.Log("bossnot playing");
            bossPlaying = false;
            bossAnimator.SetBool("isPlaying", false);
            bossAnimator.SetBool("isIdle", true);
        }
    }
}
