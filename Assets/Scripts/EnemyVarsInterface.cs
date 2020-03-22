using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyVarsInterface
{
    //Gets the number of rounds the enemy will do
    int getRoundCount();

    //Gets the fixed music note sequences of the enemy
    int[][] getMusicNotes();

    //TODO: dialogue handling probably done here 
    string getPrebattleDialogue();
}
