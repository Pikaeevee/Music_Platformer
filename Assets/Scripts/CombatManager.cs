using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
  public GameObject player;
  public GameObject enemy;
  public AudioClip errorSound;
  public AudioClip correctSound;

  public int numNotes;
  private int[] enemySequence; //Array of enemy notes, ranging from 0 to 3
  private bool playerPhase;
  private int playerNotesPlayed;
  private AudioSource audioPlayer;
  
  // Start is called before the first frame update
  void Start()
  {
    audioPlayer = GetComponent<AudioSource>();
    generateEnemySequence();
    startEnemyPhase();
  }

  void generateEnemySequence() {
    enemySequence = new int[numNotes];
    for(int i = 0; i < numNotes; i++) {
      enemySequence[i] = Random.Range(0, 3);
    }
  }

  void startEnemyPhase() {
    playerPhase = false;
    player.GetComponent<PlayerController>().canPlay = false;
    StartCoroutine(enemy.GetComponent<EnemyController>().PlayNotes(enemySequence));
  }

  IEnumerator playError() {
    audioPlayer.clip = errorSound;
    audioPlayer.Play();
    yield return new WaitForSeconds(audioPlayer.clip.length);
    startEnemyPhase();
  }

  IEnumerator playCorrect() {
    audioPlayer.clip = correctSound;
    audioPlayer.Play();
    yield return new WaitForSeconds(audioPlayer.clip.length);
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
    } else if(playerPhase) {
      if(Input.GetKeyDown("j")) {
        if(enemySequence[playerNotesPlayed] != 0) {
          playerPhase = false;
          StartCoroutine(playError());
        }
        playerNotesPlayed++;
      } 
      if(Input.GetKeyDown("k")) {
        if(enemySequence[playerNotesPlayed] != 1) {
          playerPhase = false;
          StartCoroutine(playError());
        }
        playerNotesPlayed++;
      }
      if(Input.GetKeyDown("l")) {
        if(enemySequence[playerNotesPlayed] != 2) {
          playerPhase = false;
          StartCoroutine(playError());
        }
        playerNotesPlayed++;
      }
      if(Input.GetKeyDown(";")) {
        if(enemySequence[playerNotesPlayed] != 3) {
          playerPhase = false;
          StartCoroutine(playError());
        }
        playerNotesPlayed++;
      }
      if(playerNotesPlayed == numNotes) {
        playerPhase = false;
        StartCoroutine(playCorrect());
      }
    }
  }
}
