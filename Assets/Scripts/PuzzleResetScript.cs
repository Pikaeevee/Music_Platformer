using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleResetScript : MonoBehaviour
{
    private PuzzleManager puzzle;
    private Quaternion[] puzzleInitPos; 

    private bool playerNear; 
    // Start is called before the first frame update
    void Start()
    {
        puzzle = GameObject.FindGameObjectWithTag("Puzzle").GetComponent<PuzzleManager>();
        puzzleInitPos = puzzle.initRotations;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerNear)
        {
            Debug.Log("resetting puzzle");
            int i = 0; 
            foreach (GameObject ring in puzzle.puzzleRings)
            {
                ring.transform.rotation = new Quaternion(puzzle.initRotations[i].x, puzzle.initRotations[i].y, puzzle.initRotations[i].z, puzzle.initRotations[i].w);
                i++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerNear = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerNear = false; 
        }
    }
}
