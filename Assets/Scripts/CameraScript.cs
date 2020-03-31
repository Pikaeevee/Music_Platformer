using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    /***
     * smooth follow 2d camera script from 
     * https://gist.github.com/GuilleUCM/0627c64630b745f70bd2
     * 
     */

    //offset from the viewport center to fix damping
    public float m_DampTime = 10f;
    public Transform m_Target;
    public float m_XOffset = 0;
    public float m_YOffset = 0;

    private float margin = 0.3f;

    private float resetMargin = 1.0f; 

    private float shake = 0.0f;

    private float offset = 0.3f; 

    // Start is called before the first frame update
    void Start()
    {
        if (m_Target == null)
        {
            m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target)
        {
            float targetX = m_Target.position.x + m_XOffset;
            float targetY = m_Target.position.y + m_YOffset;

            // check if player has teleported too far 
            if (Mathf.Abs(transform.position.x - targetX) > resetMargin)
            {
                targetX = m_Target.position.x; 
            }
            if (Mathf.Abs(transform.position.y - targetY) > resetMargin)
            {
                targetY = m_Target.position.y;
            }
            transform.position = new Vector3(targetX, targetY, transform.position.z);

            if (Mathf.Abs(transform.position.x - targetX) > margin)
            {
                targetX = Mathf.Lerp(transform.position.x, targetX, 1 / m_DampTime * Time.deltaTime) + shake;
            }
            if (Mathf.Abs(transform.position.y - targetY) > margin)
            {
                targetY = Mathf.Lerp(transform.position.y, targetY, m_DampTime * Time.deltaTime);
            }

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }

    public void setRelPos(float enemyXPos) {
      offset = (enemyXPos - m_Target.position.x) / 2;
    }

    public void resetRelPos() {
      offset = 0;
    }

    public IEnumerator shakeCamera() {
      for(int i = 0; i < 5; i++) {
        shake += 0.1f;
        yield return new WaitForSeconds(0.01f);
      }
      for(int i = 0; i < 5; i++) {
        shake -= 0.2f;
        yield return new WaitForSeconds(0.02f);
      }
      for(int i = 0; i < 5; i++) {
        shake += 0.1f;
        yield return new WaitForSeconds(0.01f);
      }
  }
}
