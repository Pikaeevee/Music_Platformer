using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public AudioClip[] sounds;
  public ParticleSystem[] particleSystems;
  public float delayTime;
  public bool donePlaying;
  public GameObject indicator;
  public GameObject dialogueBox;
  public CombatManager combatManager;
  public float dialogueTime = 2.0f;

  private AudioSource player;
  void Start()
  {
    player = this.GetComponent<AudioSource>();
    indicator.SetActive(false);
    dialogueBox.SetActive(false);
  }
  
  //TODO: edit this function for better dialogue support
  public IEnumerator ShowDialogue() {
    dialogueBox.SetActive(true);
    yield return new WaitForSeconds(dialogueTime);
    dialogueBox.SetActive(false);
  }

  public IEnumerator PlayNotes(int[] notes) {
    indicator.SetActive(true);
    player = this.GetComponent<AudioSource>();
    donePlaying = false;
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
}
