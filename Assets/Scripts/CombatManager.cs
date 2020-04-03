using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class CombatManager : MonoBehaviour
{
    private GameObject player;
    public GameObject enemy;
    public GameObject roundNumber;
    public AudioClip alertSound;
    public AudioClip errorSound;
    public AudioClip correctSound;

    private int numNotes;
    private int[] enemySequence; //Array of enemy notes, ranging from 0 to 3
    private bool playerPhase;
    private int playerNotesPlayed;
    private AudioSource audioPlayer;

    
    private int winCondition = 1; // # of times they have to follow correctly
    public int loseCondition = 3; // # of times they can fail before losing combat 

    private int winNum; // # of times won so far 
    private int loseNum = 0; // # of times lost so far 

    public GameObject alert;
    public GameObject playerIndicator;

    private CameraScript mainCameraScript;
    private SheetMusicManager sheetMusicManager;
    private BossManager bossManager;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        mainCameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        sheetMusicManager = GameObject.Find("SheetMusicManager").GetComponent<SheetMusicManager>();

        GameObject obj;
        if(obj = GameObject.Find("BossManager")) {
            bossManager = obj.GetComponent<BossManager>();
        } else {
            bossManager = null;
        }
    }

    public void StartCombat()
    {
        sheetMusicManager.canShowMusic = false;
        //   generateEnemySequence();
        mainCameraScript.setRelPos(enemy.transform.position.x);
        alert.GetComponent<Animator>().ResetTrigger("EnemyEncounter");
        alert.GetComponent<Animator>().SetTrigger("EnemyEncounter");
        playerPhase = false;
        player.GetComponent<PlayerController>().canPlay = false;
        PlayerManager.pm.SetPlayerState(PlayerState.fighting);
        StartCoroutine(triggerPrebattle());   
    }

    IEnumerator triggerPrebattle() {
        audioPlayer.clip = alertSound;
        audioPlayer.Play();
        yield return new WaitForSeconds(1f);
        StartCoroutine(enemy.GetComponent<EnemyController>().ShowPrebattleDialogue());
        yield return new WaitForSeconds(3.0f);
        roundNumber.SetActive(true);
        firstEnemyPhase();
    }

    //   void generateEnemySequence() {
    //     enemySequence = new int[numNotes];
    //     for(int i = 0; i < numNotes; i++) {
    //       enemySequence[i] = Random.Range(0, 4);
    //     }
    //   }

    void firstEnemyPhase() {
        winCondition = enemy.GetComponent<EnemyController>().getRoundCount();
        winNum = 0;
        updateRoundNumber();
        enemy.GetComponent<EnemyController>().StartRound();
    }

    void restartEnemyPhase() {
        enemy.GetComponent<EnemyController>().StartRound();
    }

    void startNextEnemyPhase() {
        winNum = enemy.GetComponent<EnemyController>().getCurrentRound();
        updateRoundNumber();
        enemy.GetComponent<EnemyController>().StartRound();
    }

    void updateRoundNumber() {
        roundNumber.GetComponent<TMPro.TextMeshProUGUI>().text = "<mspace=0.3em>Round " + 
        (winNum + 1) + "/" + winCondition + "</mspace>";     
    }

    IEnumerator playError() {
        loseNum++; // increment # of fails for match 
        playerPhase = false;
        StartCoroutine(mainCameraScript.shakeCamera());
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<PlayerController>().canPlay = false;
        audioPlayer.clip = errorSound;
        audioPlayer.Play();
        yield return new WaitForSeconds(audioPlayer.clip.length);

        // check if player lost match
        if (loseNum >= loseCondition)
        {
            PlayerLost();
            yield break; // stop coroutine 
        }
        playerIndicator.SetActive(false);
        restartEnemyPhase();
    }

    IEnumerator playCorrect() {
        enemy.GetComponent<EnemyController>().incrementCurrentRound();
        
        playerPhase = false;
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<PlayerController>().canPlay = false;
        audioPlayer.clip = correctSound;
        audioPlayer.Play();
        yield return new WaitForSeconds(audioPlayer.clip.length);
        // check if player won, stop coroutine if so 
        if (enemy.GetComponent<EnemyController>().isDone())
        {
            PlayerWon();
            yield break;
        }
        playerIndicator.SetActive(false);
        // only start next enemy phase if player has not yet won 
        if (winNum < winCondition)
        {
            startNextEnemyPhase();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.GetComponent<EnemyController>().donePlaying && !playerPhase) {
            playerIndicator.SetActive(true);
            playerPhase = true;
            player.GetComponent<PlayerController>().canPlay = true;
            playerNotesPlayed = 0;
            enemy.GetComponent<EnemyController>().donePlaying = false;
            enemySequence = enemy.GetComponent<EnemyController>().getCurrentNotes();
            numNotes = enemySequence.Length;
        } else if(playerPhase) {
            int ind = PlayerManager.pm.getIndexOfKey();
            if(ind != -1) {
                if(enemySequence[playerNotesPlayed] != ind) {
                    StartCoroutine(playError());
                } else {
                    playerNotesPlayed++;
                }
            }
            if(playerNotesPlayed == numNotes) {
                StartCoroutine(playCorrect());
            }
        }
    }

    IEnumerator eliminateEnemy() {
        enemy.GetComponent<Animator>().ResetTrigger("EnemyDied");
        enemy.GetComponent<Animator>().SetTrigger("EnemyDied");
        yield return new WaitForSeconds(0.2f);
        enemy.SetActive(false);
    }

    void PlayerWon()
    {
        Debug.Log("Defeated enemy!");
        winNum = 0;
        loseNum = 0; 
        playerIndicator.SetActive(false);
        mainCameraScript.resetRelPos();
        sheetMusicManager.canShowMusic = true;
        player.GetComponent<PlayerManager>().resetLives();
        PlayerManager.pm.SetPlayerState(PlayerState.playing);
        player.GetComponent<PlayerController>().canPlay = true;
        if(bossManager)
        {
            Debug.Log("MoveBoss");
            bossManager.MoveBoss();
            enemy.GetComponent<EnemyController>().resetRounds();
        }
        else 
        {
            StartCoroutine(eliminateEnemy());
        }
        roundNumber.SetActive(false);
    }

    void PlayerLost()
    {
        Debug.Log("Lost to enemy :(");
        winNum = 0;
        loseNum = 0; 
        mainCameraScript.resetRelPos();
        sheetMusicManager.canShowMusic = true;
        player.GetComponent<PlayerManager>().LoseLife(); // lose a life 
        PlayerManager.pm.SetPlayerState(PlayerState.playing);
        player.GetComponent<PlayerController>().canPlay = true;
        roundNumber.SetActive(false);
    }
}
