using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class CombatManager : MonoBehaviour
{
    private GameObject player;
    public GameObject enemy;
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
   
  
  // Start is called before the first frame update
  void Start()
  {
    audioPlayer = GetComponent<AudioSource>();
    
    player = GameObject.Find("Player");
    
  }

    public void StartCombat()
    {
        generateEnemySequence();
        playerPhase = false;
        player.GetComponent<PlayerController>().canPlay = false;
        PlayerManager.pm.playState = PlayerState.fighting;
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

  IEnumerator playError() {
    loseNum++; // increment # of fails for match 
    
    playerPhase = false;
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

    generateEnemySequence();
    startEnemyPhase();
  }

  // Update is called once per frame
  void Update()
  {
    if(enemy.GetComponent<EnemyController>().donePlaying && !playerPhase) {
      playerPhase = true;
      player.GetComponent<PlayerController>().canPlay = true;
      playerNotesPlayed = 0;
      enemy.GetComponent<EnemyController>().donePlaying = false;
    } else if(playerPhase) {
      if(Input.GetKeyDown("j")) {
        if(enemySequence[playerNotesPlayed] != 0) {
          StartCoroutine(playError());
        }
        playerNotesPlayed++;
      } 
      if(Input.GetKeyDown("k")) {
        if(enemySequence[playerNotesPlayed] != 1) {
          StartCoroutine(playError());
        }
        playerNotesPlayed++;
      }
      if(Input.GetKeyDown("l")) {
        if(enemySequence[playerNotesPlayed] != 2) {
          StartCoroutine(playError());
        }
        playerNotesPlayed++;
      }
      if(Input.GetKeyDown(";")) {
        if(enemySequence[playerNotesPlayed] != 3) {
          StartCoroutine(playError());
        }
        playerNotesPlayed++;
      }
      if(playerNotesPlayed == numNotes) {
        StartCoroutine(playCorrect());
      }
    }
  }

    void PlayerWon()
    {
        Debug.Log("Defeated enemy!");
        // TODO: UNLOCK MOVEMENT, DELETE ENEMY(??)
        PlayerManager.pm.playState = PlayerState.playing;
        player.GetComponent<PlayerController>().canPlay = true;
        enemy.SetActive(false);
    }

    void PlayerLost()
    {
        Debug.Log("Lost to enemy :(");
        winNum = 0;
        loseNum = 0; 
        player.GetComponent<PlayerManager>().LoseLife(); // lose a life 
        // TODO: UNLOCK MOVEMENT
        PlayerManager.pm.playState = PlayerState.playing;
        player.GetComponent<PlayerController>().canPlay = true;
    }
}
