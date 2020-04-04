using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheetMusicController : MonoBehaviour
{
  public int musicIndex;
  public GameObject sheetMusicManager;
  
  // Start is called before the first frame update
  void Start()
  {
      sheetMusicManager = GameObject.Find("SheetMusicManager");
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  void OnCollisionEnter2D(Collision2D col) {
    if(col.gameObject.tag == "Player") {
      Debug.Log("ahh");
      gameObject.SetActive(false);
      sheetMusicManager.GetComponent<SheetMusicManager>().setSheetMusicObtained(musicIndex);
      sheetMusicManager.GetComponent<SheetMusicManager>().displayMusic(musicIndex);
      AddMusic();
    }
  }

  private void AddMusic()
  {
    Scene scene = SceneManager.GetActiveScene();
    switch(musicIndex)
    {
      // Melodica Tutorial
      case 0:
        if(scene.name == "Level_Zero_Setup") {
            PlayerManager.pm.gameObject.GetComponent<PlayerController>().canPlay = true;
        }
        break;
      // High jump ability
      case 1:
        if(scene.name == "Level_Zero_Setup") {
            PlayerManager.pm.gameObject.GetComponent<PlayerController>().AddAbility("HighJump");
        }
        break;
      case 3:
      //Spike manip ability
        if(scene.name == "Level_Two_Setup") {
            PlayerManager.pm.gameObject.GetComponent<PlayerController>().AddAbility("SpikesControl");
        }
        break;
      default:
        break;
    }
  }
}
