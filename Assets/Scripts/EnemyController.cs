using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public AudioClip[] sounds;
  public float delayTime;
  public bool donePlaying;

  private AudioSource player;
  // Start is called before the first frame update
  void Start()
  {
    player = GetComponent<AudioSource>();
  }

  public IEnumerator PlayNotes(int[] notes) {
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
  // Update is called once per frame
  void Update()
  {
      
  }
}
