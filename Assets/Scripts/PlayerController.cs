using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public AudioClip[] sounds;
  public AudioClip buffSound;
  public AudioClip debuffSound;
  public ParticleSystem[] particleSystems;
  // public ParticleSystem notesParticles;
  public bool canPlay = false;
    // public float delayTime;

    public bool canControlSpikes = false; 

  private AudioSource player;

  // Note sequencing variables
  // private string[] allSequences = {"jkl;", "jjl;", "k;l"};
  // private string[] playerSequences = {"jkl;"}; // Will empty for game start and fill as music is found
  private Dictionary<string, string> allSequences = new Dictionary<string, string>();
  private Dictionary<string, bool> sequenceActivated = new Dictionary<string, bool>();
  private Dictionary<string, string> playerSequences = new Dictionary<string, string>();
  private float timeoutDuration = 1.0f;
  private float timeoutTime = 0.0f;
  private string userSequence = "";
  private PlayerMovement movementScript;
  private BuffIconManager buffIconManager;
  private int playedNotes = 0;
  private Animator playerAnimator;

  // Start is called before the first frame update
  void Start()
  {
    playerAnimator = GetComponent<Animator>();
    player = GetComponent<AudioSource>();
    movementScript = GetComponent<PlayerMovement>();
    buffIconManager = GameObject.Find("BuffIconManager").GetComponent<BuffIconManager>();

    allSequences.Add("ijkl", "HighJump");
    sequenceActivated.Add("HighJump", false);
    allSequences.Add("jkjk", "Dash");
    sequenceActivated.Add("Dash", false);
    allSequences.Add("jkli", "SpikesControl");
    sequenceActivated.Add("SpikesControl", false);
     // playerSequences.Add("jkl;", "HighJump");
        AddAbility("Dash");
        AddAbility("SpikesControl");
  }

  IEnumerator PlayParticleEffect(int i) {
    playerAnimator.SetBool("isPlaying", true);
    particleSystems[i].Play();
    yield return new WaitForSeconds(0.5f);
    particleSystems[i].Stop();
    playerAnimator.SetBool("isPlaying", false);
  }

  void PlaySound(int i) {
    StartCoroutine(PlayParticleEffect(i));
    playedNotes++;
    player.clip = sounds[i];
    player.Play();
  }

  IEnumerator startHighJumpTimer() {
    player.clip = buffSound;
    player.Play();
    buffIconManager.showIcon(0, 10.0f);
    yield return new WaitForSeconds(10.0f);
    movementScript.Invoke("DeactivateHighJump", 0.0f);
    sequenceActivated["HighJump"] = false;
    player.clip = debuffSound;
    player.Play();
  }

  void HighJump() {
    movementScript.Invoke("HighJump", 0.0f);
    StartCoroutine(startHighJumpTimer());
  }

  // Update is called once per frame
  void Update()
  {
    if(canPlay) {
      int ind = PlayerManager.pm.getIndexOfKey();
      if(ind != -1) {
        PlaySound(ind);
      }

      
      // Sequence Handling
      // Player has about a second to enter next key/note in sequence
      if(PlayerManager.pm.playState == PlayerState.playing)
      {
        if(Input.inputString.Length > 0) {
          timeoutTime = Time.time + timeoutDuration;
          for(int i = 0; i < Input.inputString.Length; i++)
          {
            if("ijkl".Contains(Input.inputString[i].ToString()))
            {
              userSequence += Input.inputString[i];
            }
          }
          foreach(string s in playerSequences.Keys) {
            if(userSequence == s) {
              if(!sequenceActivated[playerSequences[s]]) {
                this.Invoke(playerSequences[s], 0.0f);
                sequenceActivated[playerSequences[s]] = true;
              }
            }
          }
        }
        else if(Time.time > timeoutTime && userSequence.Length > 0) {
          userSequence = "";
        }
      }
    }
  }

    void Dash()
    {
        movementScript.canDash = true;
        sequenceActivated["Dash"] = false;
    }

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(10.0f);
        movementScript.canDash = false; 
    }

    void SpikesControl()
    {
        canControlSpikes = true;
        StartCoroutine(SpikesControlDuration());
    }

    IEnumerator SpikesControlDuration()
    {
        yield return new WaitForSeconds(5.0f);
        canControlSpikes = false; 
        sequenceActivated["SpikesControl"] = false;
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
