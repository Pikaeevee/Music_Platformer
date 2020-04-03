using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrOctaveaVars : MonoBehaviour, EnemyVarsInterface
{
    //Gets the number of rounds the enemy will do
    public int getRoundCount() {
        return 3;
    }

    //Gets the fixed music note sequences of the enemy
    public int[][] getMusicNotes() {
        int[][] musicNotes = new int[3][];
        musicNotes[0] = new int[]{2, 2, 0, 3, 1};
        musicNotes[1] = new int[]{1, 1, 2, 3, 0};
        musicNotes[2] = new int[]{0, 3, 2, 3, 1};
        return musicNotes;
    }

    public string getPrebattleDialogue() {
        return "You and that melodica have no place in my class!";
    }
}
