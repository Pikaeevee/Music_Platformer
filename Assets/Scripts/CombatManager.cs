using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
  public GameObject player;
  public GameObject enemy;

  public int numNotes;
  private int[] enemySequence; //Array of enemy notes, ranging from 0 to 3
  private bool playerPhase;
  
  // Start is called before the first frame update
  void Start()
  {
    playerPhase = false;
    generateEnemySequence();
    player.GetComponent<PlayerController>().canPlay = false;
    StartCoroutine(enemy.GetComponent<EnemyController>().PlayNotes(enemySequence));
  }

  void generateEnemySequence() {
    enemySequence = new int[numNotes];
    for(int i = 0; i < numNotes; i++) {
      enemySequence[i] = Random.Range(0, 3);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if(enemy.GetComponent<EnemyController>().donePlaying && !playerPhase) {
      playerPhase = true;
      player.GetComponent<PlayerController>().canPlay = true;
    }
  }
}
