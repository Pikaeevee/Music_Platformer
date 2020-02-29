using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public AudioClip[] sounds;
  // public float delayTime;

  private AudioSource player;
  // Start is called before the first frame update
  void Start()
  {
    player = GetComponent<AudioSource>();
  }

  IEnumerator PlaySound(AudioClip sound) {
    player.clip = sound;
    player.Play();
    yield return new WaitForSeconds(player.clip.length);
  }

  // Update is called once per frame
  void Update()
  {
    if(Input.GetKeyDown("j")) {
      StartCoroutine(PlaySound(sounds[0]));
    } 
    if(Input.GetKeyDown("k")) {
      StartCoroutine(PlaySound(sounds[1]));
    }
    if(Input.GetKeyDown("l")) {
      StartCoroutine(PlaySound(sounds[2]));
    }
    if(Input.GetKeyDown(";")) {
      StartCoroutine(PlaySound(sounds[3]));
    }
  }
}
