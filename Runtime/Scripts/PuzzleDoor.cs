using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleDoor : MonoBehaviour
{
    [SerializeField] private Camera puzzleCamera;
    [SerializeField] private Camera mainCamera;
    private void OnCollisionEnter(Collision collision)
    {
        if (QuestManager.Instance.currentQuestID == 2)
        {
            // This code is in charge of changing camera to the puzzle camera facing the maze when the player collides with the puzzle door
            puzzleCamera.enabled = true;
            mainCamera.enabled = false;
        }
    }
}
