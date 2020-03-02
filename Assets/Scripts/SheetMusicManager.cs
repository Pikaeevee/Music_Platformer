using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheetMusicManager : MonoBehaviour
{
  private bool showingMusic;

  public Sprite[] musicSprites;
  public GameObject sheetMusicImage;

  void Start()
  {
    showingMusic = false;
    sheetMusicImage.SetActive(false);
  }

  public void displayMusic(int i) {
    Time.timeScale = 0;
    showingMusic = true;
    sheetMusicImage.GetComponent<Image>().sprite = musicSprites[i];
    sheetMusicImage.SetActive(true);
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
