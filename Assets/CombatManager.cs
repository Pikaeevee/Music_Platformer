using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
  public GameObject player;
  public GameObject enemy;

  public int numNotes;
  private int[] enemySequence; //Array of enemy notes, ranging from 0 to 3
  
  // Start is called before the first frame update
  void Start()
  {
    generateEnemySequence();
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
      
  }
}
