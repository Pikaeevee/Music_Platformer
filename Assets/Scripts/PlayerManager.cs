using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState { playing, fighting }

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager pm; 

    public PlayerState playState = PlayerState.playing;

    public int playerLives = 3; 

    public string[] noteKeys = {"i", "j", "k", "l"};

    private void Awake()
    {
        // Singleton behavior 
        if (pm == null)
        {
            pm = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns index of the key pressed; if key not in array, return -1
    public int getIndexOfKey() {
      for(int i = 0; i < noteKeys.Length; i++) {
        if(Input.GetKeyDown(noteKeys[i])) return i;
      }
      return -1;
    }

    public void LoseLife()
    {
        Debug.Log("lost a life :c");
        playerLives--; 
        if (playerLives <= 0)
        {
            GameOver(); 
        }
    }

    private void GameOver()
    {
        Debug.Log("You died :(");
        // TODO: CREATE GAME OVER SCREEN  
        //SceneManager.LoadScene("Game Over"); 
    }
}
