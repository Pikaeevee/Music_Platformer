using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheetMusicManager : MonoBehaviour
{
  private bool showingMusic;

  public Sprite[] musicSprites;
  public GameObject sheetMusicImage;
  private bool[] obtainedMusic;

  void Start()
  {
    obtainedMusic = new bool[musicSprites.Length];
    showingMusic = false;
    sheetMusicImage.SetActive(false);
  }

  public void setSheetMusicObtained(int i) {
    obtainedMusic[i] = true;  
  }

  public void displayMusic(int i) {
    if(obtainedMusic[i]) {
      Time.timeScale = 0;
      showingMusic = true;
      sheetMusicImage.GetComponent<Image>().sprite = musicSprites[i];
      sheetMusicImage.SetActive(true);
    }
  }

  public void hideMusic() {
    Time.timeScale = 1;
    showingMusic = false;
    sheetMusicImage.SetActive(false);
  }

  void Update()
  {
    if(!showingMusic && Input.GetKeyDown("m")) {
      Debug.Log("Showing Music");
      displayMusic(0);
    }
    if(showingMusic && Input.GetKeyDown(KeyCode.Escape)) {
      Debug.Log("Hiding Music");
      hideMusic();
    }
  }
}
