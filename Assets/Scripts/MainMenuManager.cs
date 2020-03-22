using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(startGame);
        exitButton.onClick.AddListener(exitGame);
    }

    void startGame() {
        Debug.Log("PRESSED THE BUTTON YAY");
        loadScene(1); //LOAD THE GAME
    }

    //Loads the given scene
    public void loadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    //Exits the game
    public void exitGame()
    {
        Application.Quit();
    }  
}
