using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleScript : MonoBehaviour
{
    [SerializeField] private Camera puzzleCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject maze;
    [SerializeField] private GameObject finishLine;

    [SerializeField] private int turnSpeed;

    private void Awake()
    {
        mainCamera.enabled = true;
        puzzleCamera.enabled = false;
    }

    private void Update()
    {
        // The code here rotates the maze in the physics puzzle by using the "a" and "d" keys on the keyboard
        if (Input.GetKey("a"))
        {
            maze.transform.eulerAngles += new Vector3(0, 0, turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            maze.transform.eulerAngles -= new Vector3(0, 0, turnSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // This script is attached to the ball in the physics puzzle, meaning that it checks collision between the ball and the finish line in the maze - then the puzzle ends and cameras switch back
        if (collision.gameObject == finishLine)
        {
            QuestManager.Instance.EndQuest();
            mainCamera.enabled = true;
            puzzleCamera.enabled = false;
        }
    }
}
