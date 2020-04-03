using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCutsceneManager : MonoBehaviour
{
    public GameObject cutsceneImage;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneImage.GetComponent<Animator>().SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (cutsceneImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Finished"))
        {
            Debug.Log("Starting the game...");
            startGame();
        }
    }

    void startGame() {
        loadScene(0); //LOAD THE GAME
    }

    //Loads the given scene
    public void loadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
