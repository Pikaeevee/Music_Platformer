using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class CombatManager : MonoBehaviour
{
    private GameObject player;
    public GameObject enemy;
    public GameObject roundNumber;
    public AudioClip errorSound;
    public AudioClip correctSound;

    public int numNotes;
    private int[] enemySequence; //Array of enemy notes, ranging from 0 to 3
    private bool playerPhase;
    private int playerNotesPlayed;
    private AudioSource audioPlayer;

    
    public int winCondition = 3; // # of times they have to follow correctly
    public int loseCondition = 3; // # of times they can fail before losing combat 

    private int winNum = 0; // # of times won so far 
    private int loseNum = 0; // # of times lost so far 

    public GameObject alert;
    public GameObject playerIndicator;

    private CameraScript mainCameraScript;
    private SheetMusicManager sheetMusicManager;
  
  // Start is called before the first frame update
  void Start()
  {
    audioPlayer = GetComponent<AudioSource>();
    player = GameObject.Find("Player");
    mainCameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
    sheetMusicManager = GameObject.Find("SheetMusicManager").GetComponent<SheetMusicManager>();
  }

  public void StartCombat()
  {
      sheetMusicManager.canShowMusic = false;
      generateEnemySequence();
      mainCameraScript.setRelPos(enemy.transform.position.x);
      alert.GetComponent<Animator>().ResetTrigger("EnemyEncounter");
      alert.GetComponent<Animator>().SetTrigger("EnemyEncounter");
      playerPhase = false;
      player.GetComponent<PlayerController>().canPlay = false;
      PlayerManager.pm.playState = PlayerState.fighting;
      StartCoroutine(triggerPrebattle());   
  }

  IEnumerator triggerPrebattle() {
    yield return new WaitForSeconds(1f);
    // StartCoroutine(enemy.GetComponent<EnemyController>().ShowDialogue());
    // yield return new WaitForSeconds(3.0f);
    roundNumber.SetActive(true);
    updateRoundNumber();
    startEnemyPhase();
  }

  void generateEnemySequence() {
    enemySequence = new int[numNotes];
    for(int i = 0; i < numNotes; i++) {
      enemySequence[i] = Random.Range(0, 4);
    }
  }

  void startEnemyPhase() {
    StartCoroutine(enemy.GetComponent<EnemyController>().PlayNotes(enemySequence));
  }

  void updateRoundNumber() {
    roundNumber.GetComponent<TMPro.TextMeshProUGUI>().text = "<mspace=0.3em>Round " + 
      (winNum + 1) + "/" + winCondition + "</mspace>";     
  }

  IEnumerator playError() {
    loseNum++; // increment # of fails for match 
    playerPhase = false;
    StartCoroutine(mainCameraScript.shakeCamera());
    yield return new WaitForSeconds(0.2f);
    player.GetComponent<PlayerController>().canPlay = false;
    audioPlayer.clip = errorSound;
    audioPlayer.Play();
    yield return new WaitForSeconds(audioPlayer.clip.length);

    // check if player lost match
    if (loseNum >= loseCondition)
    {
        PlayerLost();
        yield break; // stop coroutine 
    }
    playerIndicator.SetActive(false);
    startEnemyPhase();
  }

  IEnumerator playCorrect() {
    winNum++; // increment # of successes for match 

    playerPhase = false;
    yield return new WaitForSeconds(0.2f);
    player.GetComponent<PlayerController>().canPlay = false;
    audioPlayer.clip = correctSound;
    audioPlayer.Play();
    yield return new WaitForSeconds(audioPlayer.clip.length);
    // check if player won, stop coroutine if so 
    if (winNum >= winCondition)
    {
        PlayerWon();
        yield break; 
    }
    playerIndicator.SetActive(false);
    updateRoundNumber();
    generateEnemySequence();
    startEnemyPhase();
  }

  // Update is called once per frame
  void Update()
  {
    if(enemy.GetComponent<EnemyController>().donePlaying && !playerPhase) {
      playerIndicator.SetActive(true);
      playerPhase = true;
      player.GetComponent<PlayerController>().canPlay = true;
      playerNotesPlayed = 0;
      enemy.GetComponent<EnemyController>().donePlaying = false;
    } else if(playerPhase) {
      int ind = PlayerManager.pm.getIndexOfKey();
      if(ind != -1) {
        if(enemySequence[playerNotesPlayed] != ind) {
          StartCoroutine(playError());
        } else {
          playerNotesPlayed++;
        }
      }
      // TODO: delete this code if combat works out
      // if(Input.GetKeyDown("j")) {
      //   if(enemySequence[playerNotesPlayed] != 0) {
      //     StartCoroutine(playError());
      //   }
      //   playerNotesPlayed++;
      // } 
      // if(Input.GetKeyDown("k")) {
      //   if(enemySequence[playerNotesPlayed] != 1) {
      //     StartCoroutine(playError());
      //   }
      //   playerNotesPlayed++;
      // }
      // if(Input.GetKeyDown("l")) {
      //   if(enemySequence[playerNotesPlayed] != 2) {
      //     StartCoroutine(playError());
      //   }
      //   playerNotesPlayed++;
      // }
      // if(Input.GetKeyDown(";")) {
      //   if(enemySequence[playerNotesPlayed] != 3) {
      //     StartCoroutine(playError());
      //   }
      //   playerNotesPlayed++;
      // }
      if(playerNotesPlayed == numNotes) {
        StartCoroutine(playCorrect());
      }
    }
  }

  IEnumerator eliminateEnemy() {
    enemy.GetComponent<Animator>().ResetTrigger("EnemyDied");
    enemy.GetComponent<Animator>().SetTrigger("EnemyDied");
    yield return new WaitForSeconds(0.2f);
    enemy.SetActive(false);
  }

    void PlayerWon()
    {
        Debug.Log("Defeated enemy!");
        winNum = 0;
        loseNum = 0; 
        playerIndicator.SetActive(false);
        mainCameraScript.resetRelPos();
        sheetMusicManager.canShowMusic = true;
        // TODO: UNLOCK MOVEMENT, DELETE ENEMY(??)
        player.GetComponent<PlayerManager>().resetLives();
        PlayerManager.pm.playState = PlayerState.playing;
        player.GetComponent<PlayerController>().canPlay = true;
        StartCoroutine(eliminateEnemy());
        roundNumber.SetActive(false);
    }

    void PlayerLost()
    {
        Debug.Log("Lost to enemy :(");
        winNum = 0;
        loseNum = 0; 
        mainCameraScript.resetRelPos();
        sheetMusicManager.canShowMusic = true;
        player.GetComponent<PlayerManager>().LoseLife(); // lose a life 
        PlayerManager.pm.playState = PlayerState.playing;
        player.GetComponent<PlayerController>().canPlay = true;
        roundNumber.SetActive(false);
    }
}
