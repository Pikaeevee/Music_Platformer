using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 viewGoal;
    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;

    public float smoothTime = 0.5f;

    private float shake = 0.0f;
    private float offset = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerposition = player.transform.position;
        Vector3 cameraposition = transform.position;
        cameraposition.x = Mathf.Clamp(Mathf.SmoothDamp(cameraposition.x, playerposition.x + offset, ref xVelocity, smoothTime) + shake, 0, 256);
        if(Mathf.Abs(playerposition.y - cameraposition.y) > 2.5f)
        {
            cameraposition.y = Mathf.SmoothDamp(cameraposition.y, playerposition.y, ref yVelocity, smoothTime);
        }
        transform.position = cameraposition;
    }

    public void setRelPos(float enemyXPos) {
      offset = (enemyXPos - player.transform.position.x) / 2;
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
