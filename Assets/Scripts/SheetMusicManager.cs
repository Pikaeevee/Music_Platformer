using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SheetMusicManager : MonoBehaviour
{
  public Sprite[] musicSprites;
  private GameObject sheetMusicImage;
  public bool canShowMusic;
  private bool[] obtainedMusic;
  private bool showingMusic;
  private bool recallingMusic;
  private int currentMusicIndex;
  private bool can_switch;
  private AudioSource audioSource;
  public AudioClip pullUpMusicSound;
  public AudioClip changeMusicSound;
  public AudioClip putAwayMusicSound;

    // public static SheetMusicManager sm; 

    private void Awake()
    {
        // Singleton behavior 
        // if (sm == null)
        // {
        //     sm = this;
        //     DontDestroyOnLoad(this.gameObject);
        // }
        // else
        // {
        //     Destroy(this.gameObject);
        // }
    }

    void Start()
    {
        Debug.Log("aaah");
        canShowMusic = true;
        currentMusicIndex = 0;
        showingMusic = false;
        sheetMusicImage = GameObject.Find("SheetMusicImage");
        audioSource = this.GetComponent<AudioSource>();
    }

  public void setSheetMusicObtained(int i) {
    obtainedMusic[i] = true;  
  }

  public void displayMusic(int i) {
    if(obtainedMusic[i]) {
      PlayerManager.pm.SetPlayerState(PlayerState.menuing);
      showingMusic = true;
      sheetMusicImage.GetComponent<Image>().sprite = musicSprites[i];
      sheetMusicImage.SetActive(true);
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

    IEnumerator PutAwayMusic()
    {
        audioSource.clip = putAwayMusicSound;
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        PlayerManager.pm.SetPlayerState(PlayerState.playing);
    }

    public void hideMusic() {
        showingMusic = false;
        recallingMusic = false;
        sheetMusicImage.GetComponent<Animator>().ResetTrigger("FadeUp");
        sheetMusicImage.GetComponent<Animator>().SetBool("HideMusic", true);
        StartCoroutine(PutAwayMusic());
    }

    void Update()
    {
        if(!showingMusic && canShowMusic && Input.GetKeyDown("m")) {
        Debug.Log("Showing Music");
        recallingMusic = true;
        displayMusic(0);
        }
        if(showingMusic && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))) {
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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { 
        obtainedMusic = new bool[musicSprites.Length];

        if (scene.name == "MainMenu")
        {
            // Destroy the gameobject this script is attached to
            Destroy(gameObject);
        }
        if(scene.name != "Level_Zero_Setup") {
            if(scene.name == "Level_One_Setup") {
                for(int i = 0; i < 2; i++) {
                    obtainedMusic[i] = true;
                    canShowMusic = true;
                }
            }
            if(scene.name == "Level_Two_Setup") {
                for(int i = 0; i < 3; i++) {
                    obtainedMusic[i] = true;
                    canShowMusic = true;
                }
            }
            if(scene.name == "Boss_Level_Setup") {
                for(int i = 0; i < obtainedMusic.Length; i++) {
                    obtainedMusic[i] = true;
                    canShowMusic = true;
                }
            }
        }
        
    }
}
