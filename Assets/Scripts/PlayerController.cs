using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public AudioClip[] sounds;
  public ParticleSystem[] particleSystems;
  // public ParticleSystem notesParticles;
  public bool canPlay = false;
  // public float delayTime;

  private AudioSource player;

  // Note sequencing variables
  // private string[] allSequences = {"jkl;", "jjl;", "k;l"};
  // private string[] playerSequences = {"jkl;"}; // Will empty for game start and fill as music is found
  private Dictionary<string, string> allSequences = new Dictionary<string, string>();
  private Dictionary<string, string> playerSequences = new Dictionary<string, string>();
  private float timeoutDuration = 1.0f;
  private float timeoutTime = 0.0f;
  private string userSequence = "";
  private PlayerMovement movementScript;

  private int playedNotes = 0;

  // Start is called before the first frame update
  void Start()
  {
    player = GetComponent<AudioSource>();
    movementScript = GetComponent<PlayerMovement>();
    allSequences.Add("jkl;", "HighJump");
    allSequences.Add("jjl;", "AnotherAbility");
    allSequences.Add("k;l", "AnotherAbility");
    // playerSequences.Add("jkl;", "HighJump");
  }

  IEnumerator PlayParticleEffect(int i) {
    particleSystems[i].Play();
    yield return new WaitForSeconds(0.5f);
    particleSystems[i].Stop();
  }

  void PlaySound(int i) {
    StartCoroutine(PlayParticleEffect(i));
    playedNotes++;
    player.clip = sounds[i];
    player.Play();
  }

  // Update is called once per frame
  void Update()
  {
    if(canPlay) {
      if(Input.GetKeyDown("j")) {
        PlaySound(0);
      } 
      if(Input.GetKeyDown("k")) {
        PlaySound(1);
      }
      if(Input.GetKeyDown("l")) {
        PlaySound(2);
      }
      if(Input.GetKeyDown(";")) {
        PlaySound(3);
      }

      
      // Sequence Handling
      // Player has about a second to enter next key/note in sequence
      if(PlayerManager.pm.playState == PlayerState.playing)
      {
        if(Input.inputString.Length > 0) {
          timeoutTime = Time.time + timeoutDuration;
          for(int i = 0; i < Input.inputString.Length; i++)
          {
            if("jkl;".Contains(Input.inputString[i].ToString()))
            {
              userSequence += Input.inputString[i];
            }
          }
          foreach(string s in playerSequences.Keys) {
            if(userSequence == s) {
              movementScript.Invoke(playerSequences[s], 0.0f);
            }
          }
        }
        else if(Time.time > timeoutTime && userSequence.Length > 0) {
          userSequence = "";
        }
      }
    }
  }

  public void AddAbility(string ability)
  {
    foreach(string s in allSequences.Keys)
    {
      if(ability == allSequences[s])
      {
        playerSequences.Add(s, allSequences[s]);
        break;
      }
    }
  }
}
