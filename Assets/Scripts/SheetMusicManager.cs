using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheetMusicManager : MonoBehaviour
{
  public Sprite[] musicSprites;
  public GameObject sheetMusicImage;
  public GameObject escapeToCloseText;
  public GameObject mToMusicText;
  public bool canShowMusic;
  private bool[] obtainedMusic;
  private bool showingMusic;
  private bool recallingMusic;
  private int currentMusicIndex;
  private bool can_switch;

  private GameObject combatUI;
  private GameObject sheetMusicUI;

  private AudioSource audioSource;
  public AudioClip pullUpMusicSound;
  public AudioClip changeMusicSound;
  public AudioClip putAwayMusicSound;


  void Start()
  {
    canShowMusic = true;
    obtainedMusic = new bool[musicSprites.Length];
    currentMusicIndex = 0;
    showingMusic = false;
    escapeToCloseText.SetActive(false);
    mToMusicText.SetActive(false);
    combatUI = GameObject.Find("Combat UI");
    sheetMusicUI = GameObject.Find("SheetMusicUI");
    audioSource = this.GetComponent<AudioSource>();
  }

  public void setSheetMusicObtained(int i) {
    obtainedMusic[i] = true;  
  }

  public void displayMusic(int i) {
    if(obtainedMusic[i]) {
      Time.timeScale = 0;
      combatUI.SetActive(false);
      showingMusic = true;
      sheetMusicImage.GetComponent<Image>().sprite = musicSprites[i];
      sheetMusicImage.SetActive(true);
      escapeToCloseText.SetActive(true);
      mToMusicText.SetActive(false);
      sheetMusicImage.GetComponent<Animator>().SetTrigger("FadeUp");
      sheetMusicImage.GetComponent<Animator>().SetBool("HideMusic", false);
      currentMusicIndex = i;
      audioSource.clip = pullUpMusicSound;
      audioSource.Play();
    }
  }

  public void changeToNextMusic() {
    for(int i = 1; i < obtainedMusic.Length; i++) {
      int newIndex = (currentMusicIndex + i) % obtainedMusic.Length;
      if(obtainedMusic[newIndex]) {
        sheetMusicImage.GetComponent<Image>().sprite = musicSprites[newIndex];
        sheetMusicImage.GetComponent<Animator>().ResetTrigger("NextMusic");
        sheetMusicImage.GetComponent<Animator>().SetTrigger("NextMusic");
        currentMusicIndex = newIndex;
        audioSource.clip = changeMusicSound;
        audioSource.Play();
        return;
      }
    }
  }

  public void changeToPrevMusic() {
    for(int i = 1; i < obtainedMusic.Length; i++) {
      int newIndex = (currentMusicIndex + obtainedMusic.Length - i) % obtainedMusic.Length;
      if(obtainedMusic[newIndex]) {
        sheetMusicImage.GetComponent<Image>().sprite = musicSprites[newIndex];
        sheetMusicImage.GetComponent<Animator>().ResetTrigger("PrevMusic");
        sheetMusicImage.GetComponent<Animator>().SetTrigger("PrevMusic");
        currentMusicIndex = newIndex;
        audioSource.clip = changeMusicSound;
        audioSource.Play();
        return;
      }
    }
  }

  public void hideMusic() {
    Time.timeScale = 1;
    combatUI.SetActive(true);
    showingMusic = false;
    recallingMusic = false;
    escapeToCloseText.SetActive(false);
    mToMusicText.SetActive(true);
    sheetMusicImage.GetComponent<Animator>().ResetTrigger("FadeUp");
    sheetMusicImage.GetComponent<Animator>().SetBool("HideMusic", true);
    audioSource.clip = putAwayMusicSound;
    audioSource.Play();
  }

  void Update()
  {
    if(!canShowMusic) {
      sheetMusicUI.SetActive(false);
    } else {
      sheetMusicUI.SetActive(true);
    }

    if(!showingMusic && canShowMusic && Input.GetKeyDown("m")) {
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
