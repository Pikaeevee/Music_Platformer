using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static Canvas combatUI;
    public static Canvas sheetMusicUI;
    public static Canvas playingUI;
    public static Canvas generalUI;

    void Start() {
        combatUI = GameObject.Find("CombatUI").GetComponent<Canvas>();
        sheetMusicUI = GameObject.Find("SheetMusicUI").GetComponent<Canvas>();
        playingUI = GameObject.Find("PlayingUI").GetComponent<Canvas>();
        generalUI = GameObject.Find("GeneralUI").GetComponent<Canvas>();
    }

    public static void updateUI(PlayerState state) {
        switch(state) {
            case PlayerState.menuing: 
                combatUI.enabled = false;
                playingUI.enabled = false;
                generalUI.enabled = false;
                sheetMusicUI.enabled = true;
                break;
            case PlayerState.fighting: 
                combatUI.enabled = true;
                playingUI.enabled = false;
                generalUI.enabled = true;
                sheetMusicUI.enabled = false;
                break;
            default: 
                combatUI.enabled = false;
                playingUI.enabled = true;
                generalUI.enabled = true;
                sheetMusicUI.enabled = false;
                break;
        }
    }
}
