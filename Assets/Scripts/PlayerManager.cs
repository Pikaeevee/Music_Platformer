﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager pm; 

    public enum playerState { playing, fighting }

    public int playerLives = 3; 

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