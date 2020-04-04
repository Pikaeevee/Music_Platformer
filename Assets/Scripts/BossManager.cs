using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossManager : MonoBehaviour
{
    public GameObject boss;
    public float x1, x2, x3, y1, y2, y3;
    public Canvas BossUI;
    private Animator timerAnimator;
    private TextMeshProUGUI timer;
    private Vector3 battlePos1;
    private Vector3 battlePos2;
    private Vector3 battlePos3;
    private int battlePos = 0;
    private Vector3 target;
    private float distance;
    private float startTime;
    private float speed = 0.05f;

    private int timeLimit = 200;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        target = boss.transform.position;
        battlePos1 = new Vector3(x1, y1, 0);
        battlePos2 = new Vector3(x2, y2, 0);
        battlePos3 = new Vector3(x3, y3, 0);
        timer = BossUI.GetComponentInChildren<TextMeshProUGUI>();
        timerAnimator = BossUI.GetComponentInChildren<Animator>();
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {   
        distance = Vector3.Distance(boss.transform.position, target);
        float distTraveled = (Time.time - startTime) * speed;
        if (distance != 0) {
            float increment = distTraveled / distance;
            boss.transform.position = Vector3.Lerp(boss.transform.position, target, increment);
        }
    }

    public void MoveBoss()
    {
        battlePos++;
        switch (battlePos)
        {
            case 1:
                target = battlePos1;
                break;
            case 2:
                target = battlePos2;
                break;
            case 3:
                target = battlePos3;
                break;
            case 4:
                PlayerManager.pm.NextLevel();
                break;
        }
    }
    
    public IEnumerator Countdown()
    {
        bool stressTime = false;
        while(timeLimit > 0)
        {
            if (!stressTime && timeLimit < 30) {
                stressTime = true;
                timer.color = new Color32(255, 46, 31, 255);
                timerAnimator.SetTrigger("Flash");
            }
            int min = timeLimit / 60;
            int sec = timeLimit % 60;
            if (sec < 10)
            {
                timer.text = "Time Until Expulsion: " + min + ":0" + sec;    
            }
            else
            {
                timer.text = "Time Until Expulsion: " + min + ":" + sec;
            }
            yield return new WaitForSeconds(1.0f);
            timeLimit--;
        }
        PlayerManager.pm.GameOver();
    }
}
