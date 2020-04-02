using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    private bool isSolving = false;

    public GameObject purpleRing;
    public GameObject pinkRing;
    public GameObject blueRing;
    public GameObject orangeRing;
    public GameObject[] puzzleRings;

    public Quaternion[] initRotations; 

    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(isSolving);
        puzzleRings = new GameObject[] {purpleRing, pinkRing, blueRing, orangeRing};
        initRotations = new Quaternion[4];
        initRotations[0] = purpleRing.transform.rotation;
        initRotations[1] = pinkRing.transform.rotation;
        initRotations[2] = blueRing.transform.rotation;
        initRotations[3] = orangeRing.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSolving && !Solved())
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                purpleRing.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                pinkRing.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                blueRing.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                orangeRing.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                PlayerManager.pm.NextLevel();
            }
        }
        else if (Solved())
        {
            Debug.Log("solved puzzle");
            PlayerManager.pm.NextLevel();
        }
    }

    public void SetIsSolving(bool solving)
    {
        isSolving = solving;
        Debug.Log(isSolving);
    }

    bool Solved()
    {
        foreach(GameObject ring in puzzleRings)
        {
            if (ring.transform.rotation != Quaternion.Euler(0,0,0))
            {
                return false;
            }
        }
        return true;
    }
}
