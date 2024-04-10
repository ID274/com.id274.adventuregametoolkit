using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestNPC : MonoBehaviour
{
    public bool talked;
    [SerializeField] private GameObject dialogueMenu;
    [SerializeField] private DialogueScript dialogueScript;
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        dialogueScript = dialogueMenu.GetComponent<DialogueScript>();
        talked = false; // "talked" boolean is used to check if the player has interacted with the questNPC
    }

    void Update()
    {
        // The code below is used to check if the player has clicked on the NPC with the left mouse button. If the NPC is clicked, the dialogue menu opens.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint) && hitPoint.transform.name == this.name)
            {
                dialogueMenu.SetActive(true);
                if (QuestManager.Instance.complete)
                {
                    dialogueScript.OnQuestComplete();
                }
                
            }
        }
    }

}
