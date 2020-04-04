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
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossController.donePlaying && !bossPlaying)
        {
            bossPlaying = true;
            BossAnimator.ResetTrigger("Idle");
            BossAnimator.SetTrigger("Playing"); 
        }
        else if (bossController.donePlaying && bossPlaying)
        {
            bossPlaying = false;
            BossAnimator.ResetTrigger("Playing");
            BossAnimator.SetTrigger("Idle"); 
        }
    }
}
