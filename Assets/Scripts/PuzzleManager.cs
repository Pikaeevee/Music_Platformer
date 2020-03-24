﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    private bool isSolving = false;
    private bool solved = false;

    public GameObject purpleRing;
    public GameObject pinkRing;
    public GameObject blueRing;
    public GameObject orangeRing;
    private GameObject[] puzzleRings;

    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(isSolving);
        puzzleRings = new GameObject[] {purpleRing, pinkRing, blueRing, orangeRing};
    }

    // Update is called once per frame
    void Update()
    {
        if (isSolving && !solved)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                purpleRing.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                pinkRing.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                blueRing.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                orangeRing.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
            }
        }

    }

    public void SetIsSolving(bool solving)
    {
        isSolving = solving;
        Debug.Log(isSolving);
    }
}
