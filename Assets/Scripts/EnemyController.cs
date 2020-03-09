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
  public CombatManager combatManager;

  private AudioSource player;
  void Start()
  {
    player = this.GetComponent<AudioSource>();
    indicator.SetActive(false);
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
