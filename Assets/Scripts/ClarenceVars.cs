using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClarenceVars : MonoBehaviour, EnemyVarsInterface
{
    //Gets the number of rounds the enemy will do
    public int getRoundCount() {
        return 1;
    }

    //Gets the fixed music note sequences of the enemy
    public int[][] getMusicNotes() {
        int[][] musicNotes = new int[1][];
        musicNotes[0] = new int[]{1, 1, 2, 3, 0};
        return musicNotes;
    }

    public string getPrebattleDialogue() {
        return "You think a melodica will be able to match my beautiful sounds?!";
    }
}
