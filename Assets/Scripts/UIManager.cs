using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static Canvas combatUI;
    public static Canvas sheetMusicUI;
    public static Canvas playingUI;
    public static Canvas generalUI;

    private static Image playerHealth;
    private static Image noteKeysUI;
    private static Text mToMusic;

    private static bool hasOpenedMusic = false;
    
    void Start() {
        combatUI = GameObject.Find("CombatUI").GetComponent<Canvas>();
        sheetMusicUI = GameObject.Find("SheetMusicUI").GetComponent<Canvas>();
        playingUI = GameObject.Find("PlayingUI").GetComponent<Canvas>();
        generalUI = GameObject.Find("GeneralUI").GetComponent<Canvas>();

        noteKeysUI = GameObject.Find("NoteKeys").GetComponent<Image>();
        playerHealth = GameObject.Find("PlayerHealth").GetComponent<Image>();
        mToMusic = GameObject.Find("MToMusic").GetComponent<Text>();
    }

    public static void updateUI(PlayerState state) {
        switch(state) {
            case PlayerState.menuing: 
                hasOpenedMusic = true;
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
                if(hasOpenedMusic) {
                    noteKeysUI.enabled = true;
                    mToMusic.enabled = true;
                }
                combatUI.enabled = false;
                playingUI.enabled = true;
                generalUI.enabled = true;
                sheetMusicUI.enabled = false;
                break;
        }
    }
}
