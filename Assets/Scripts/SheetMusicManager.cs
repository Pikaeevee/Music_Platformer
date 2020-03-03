using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheetMusicManager : MonoBehaviour
{
  public Sprite[] musicSprites;
  public GameObject sheetMusicImage;
  private bool[] obtainedMusic;
  private bool showingMusic;
  private bool recallingMusic;
  private int currentMusicIndex;
  private bool can_switch;

  void Start()
  {
    obtainedMusic = new bool[musicSprites.Length];
    currentMusicIndex = 0;
    showingMusic = false;
    // sheetMusicImage.SetActive(false);
  }

  public void setSheetMusicObtained(int i) {
    obtainedMusic[i] = true;  
  }

  public void displayMusic(int i) {
    if(obtainedMusic[i]) {
      Time.timeScale = 0;
      showingMusic = true;
      sheetMusicImage.GetComponent<Image>().sprite = musicSprites[i];
      // sheetMusicImage.SetActive(true);
      sheetMusicImage.GetComponent<Animator>().SetTrigger("FadeUp");
      sheetMusicImage.GetComponent<Animator>().SetBool("HideMusic", false);
      currentMusicIndex = i;
    }
  }

  public void changeToNextMusic() {
    for(int i = 1; i < obtainedMusic.Length; i++) {
      int newIndex = (currentMusicIndex + i) % obtainedMusic.Length;
      if(obtainedMusic[newIndex]) {
        sheetMusicImage.GetComponent<Image>().sprite = musicSprites[newIndex];
        currentMusicIndex = newIndex;
        return;
      }
    }
  }

  public void changeToPrevMusic() {
    for(int i = 1; i < obtainedMusic.Length; i++) {
      int newIndex = (currentMusicIndex + obtainedMusic.Length - i) % obtainedMusic.Length;
      if(obtainedMusic[newIndex]) {
        sheetMusicImage.GetComponent<Image>().sprite = musicSprites[newIndex];
        currentMusicIndex = newIndex;
        return;
      }
    }
  }

  public void hideMusic() {
    Time.timeScale = 1;
    showingMusic = false;
    recallingMusic = false;
    // sheetMusicImage.SetActive(false);
    sheetMusicImage.GetComponent<Animator>().ResetTrigger("FadeUp");
    sheetMusicImage.GetComponent<Animator>().SetBool("HideMusic", true);
  }

  void Update()
  {
    if(!showingMusic && Input.GetKeyDown("m")) {
      Debug.Log("Showing Music");
      recallingMusic = true;
      displayMusic(0);
    }
    if(showingMusic && Input.GetKeyDown(KeyCode.Escape)) {
      Debug.Log("Hiding Music");
      hideMusic();
    }
    if(recallingMusic) {
      if (Input.GetAxisRaw("Horizontal") < -0.5f && can_switch)
        {
          Debug.Log("prev");
            changeToPrevMusic();
            can_switch = false;
        }
        if (Input.GetAxisRaw("Horizontal") > 0.5f && can_switch)
        {
          Debug.Log("next");
            changeToNextMusic();
            can_switch = false;
        }
        if(Input.GetAxisRaw("Horizontal")<0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            Debug.Log("reset");
            can_switch = true;
        }
    }
  }
}
