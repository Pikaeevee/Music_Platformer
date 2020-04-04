using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrOctaveaVars : MonoBehaviour, EnemyVarsInterface
{
    private int encounterNum;

    void Start() {
        encounterNum = 0;
    }

    //Gets the number of rounds the enemy will do
    public int getRoundCount() {
        return 3;
    }

    //Gets the fixed music note sequences of the enemy
    public int[][] getMusicNotes() {
        int[][] musicNotes = new int[3][];
        switch(encounterNum) {
            case 1: {
                musicNotes[0] = new int[]{2, 2, 2, 0};
                musicNotes[1] = new int[]{1, 1, 2, 3, 0};
                musicNotes[2] = new int[]{0, 3, 2, 3, 1};
                break;
            }
            case 2: {
                musicNotes[0] = new int[]{2, 2, 0, 3, 1};
                musicNotes[1] = new int[]{3, 3, 2, 3, 0};
                musicNotes[2] = new int[]{1, 3, 2, 0, 1};
                break;
            }
            case 3: {
                musicNotes[0] = new int[]{3, 1, 3, 3, 2};
                musicNotes[1] = new int[]{0, 1, 3, 2, 1, 0};
                musicNotes[2] = new int[]{2, 2, 0, 3, 1, 3};
                break;
            }
            default: break;
        }
        return musicNotes;
    }

    public string getPrebattleDialogue() {
        encounterNum++; // DIALOGUE GOTTEN BEFORE MUSIC NOTES EVERY TIME
        switch(encounterNum) {
            case 1: {
                return "You and that melodica have no place in my class!";
            }
            case 2: {
                return "I will expell you and that sorry excuse for an instrument!";
            }
            case 3: {
                return "This is your final warning Keyton! Say goodbye to your music career!!";
            }
            default: break;
        }
        return "";
    }
}
