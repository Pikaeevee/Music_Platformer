using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetMusicController : MonoBehaviour
{
  public int musicIndex;
  public GameObject sheetMusicManager;
  // Start is called before the first frame update
  void Start()
  {
      
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  void OnCollisionEnter2D(Collision2D col) {
    if(col.gameObject.tag == "Player") {
      Debug.Log("ahh");
      gameObject.SetActive(false);
      sheetMusicManager.GetComponent<SheetMusicManager>().displayMusic(musicIndex);
      //SHOW THE SHEET MUSIC HERE
    }
  }
}
