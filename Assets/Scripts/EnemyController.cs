using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public AudioClip[] sounds;
  public float delayTime;
  public bool donePlaying;

  private AudioSource player;
  void Start()
  {
    player = this.GetComponent<AudioSource>();
  }

  public IEnumerator PlayNotes(int[] notes) {
    player = this.GetComponent<AudioSource>();
    donePlaying = false;
    for(int i = 0; i < notes.Length; i++) {
      int noteNum = notes[i];
      Debug.Log(noteNum);
      player.clip = sounds[noteNum];
      player.Play();
      yield return new WaitForSeconds(player.clip.length + delayTime);
    }
    donePlaying = true;
  }
  
  void Update()
  {
      
  }
}
