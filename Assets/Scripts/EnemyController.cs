using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
    {
    public AudioClip[] sounds;
    public ParticleSystem[] particleSystems;
    public float delayTime;
    public bool donePlaying;
    public GameObject indicator;
    public GameObject dialogueBox;
    public Text dialogueText;
    public CombatManager combatManager;
    public float dialogueTime = 2.0f;

    private AudioSource player;

    //Fixed note sequences
    private int[][] musicNotes;
    //Number of rounds
    private int roundCount;

    private int currentRound;

    private EnemyVarsInterface enemyVars;

    void Start()
    {
        player = this.GetComponent<AudioSource>();
        indicator.SetActive(false);
        dialogueBox.SetActive(false);
        enemyVars = this.GetComponent<EnemyVarsInterface>();
        roundCount = enemyVars.getRoundCount();
        currentRound = 0;
    }

    //Displays the enemy's dialogue
    public IEnumerator ShowPrebattleDialogue() {
        dialogueText.text = enemyVars.getPrebattleDialogue();
        musicNotes = enemyVars.getMusicNotes();
        dialogueBox.SetActive(true);
        yield return new WaitForSeconds(dialogueTime);
        dialogueBox.SetActive(false);
    }

    //Plays enemy's fixed sequence of notes
    public IEnumerator PlayNotes() {
        indicator.SetActive(true);
        player = this.GetComponent<AudioSource>();
        donePlaying = false;
        int[] notes = musicNotes[currentRound];
        for(int i = 0; i < notes.Length; i++) {
            int noteNum = notes[i];
            Debug.Log(noteNum);
            particleSystems[noteNum].Play();
            player.clip = sounds[noteNum];
            player.Play();
            yield return new WaitForSeconds(player.clip.length + delayTime);
        }
        donePlaying = true;
        indicator.SetActive(false);
    }

    //Moves enemy onto the next round
    public void StartRound() {
        StartCoroutine(PlayNotes());
    }
    
    //Gets the number of rounds this enemy will play
    public int getRoundCount() {
        return roundCount;
    }

    //Returns the current round number we are on
    public int getCurrentRound() {
        return currentRound;
    }

    //Returns the array of notes for the current round
    public int[] getCurrentNotes() {
        return musicNotes[currentRound];
    }

    //Increment the current round count
    public void incrementCurrentRound() {
        currentRound++;
    }

    public void resetRounds()
    {
        roundCount = enemyVars.getRoundCount();
        currentRound = 0;
    }

    //Checks if enemy is done with all rounds
    public bool isDone() {
        Debug.Log("checking if done" + roundCount + " / " + currentRound);
        return currentRound == roundCount;
    }
}
