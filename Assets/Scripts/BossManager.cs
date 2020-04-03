using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossManager : MonoBehaviour
{

    public Canvas BossUI;
    private TextMeshProUGUI timer;
    private int timeLimit = 180;

    // Start is called before the first frame update
    void Start()
    {
        timer = BossUI.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Countdown()
    {
        while(timeLimit > 0)
        {
            int min = timeLimit / 60;
            int sec = timeLimit % 60;
            if (sec < 10)
            {
                timer.text = "Don't Get Expelled!\n" + min + ":0" + sec;    
            }
            else
            {
                timer.text = "Don't Get Expelled!\n" + min + ":" + sec;
            }
            yield return new WaitForSeconds(1.0f);
            timeLimit--;
        }
        PlayerManager.pm.GameOver();
    }
}
