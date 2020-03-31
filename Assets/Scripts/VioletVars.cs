using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VioletVars : MonoBehaviour, EnemyVarsInterface
{
    //Gets the number of rounds the enemy will do
    public int getRoundCount() {
        return 1;
    }

    //Gets the fixed music note sequences of the enemy
    public int[][] getMusicNotes() {
        int[][] musicNotes = new int[1][];
        musicNotes[0] = new int[]{0, 1, 0, 2, 0, 3, 3};
        return musicNotes;
    }

    public string getPrebattleDialogue() {
        return "No plastic piano can compare with the sound of a Stradivarius!";
    }
}
