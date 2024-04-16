using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [Header("Quest Manager")]
    public int currentQuestID;
    public Quest currentQuest;
    [SerializeField] private Quest[] quests;
    public QuestNPC[] questNPC;
    public GameObject[] NPC;
    public bool complete;
    [SerializeField] private Item currentItem;
    [SerializeField] private GameObject[] berrybush;
    [SerializeField] private DialogueScript dialogueScript;


    [Header("Quest Display")]
    [SerializeField] private TextMeshProUGUI questNameText;
    [SerializeField] private TextMeshProUGUI questDescriptionText;

    void Start()
    {
        questNPC[0] = NPC[0].GetComponent<QuestNPC>();
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        questNameText.text = "";
        questDescriptionText.text = "";
        currentQuestID = 0;
        QuestBeginning();
        DisplayQuest();
    }

    void Update()
    {
        if (currentQuest.item != null && currentItem != currentQuest.item)
        {
            currentItem = currentQuest.item;
        }
        if (questNPC[0].talked)
        {
            Debug.Log("QuestNPC talked");
            questNPC[0].talked = false;
            NextQuest();
        }
        if (currentQuestID <= quests.Length)
        {
            // This bit checks if the next quest isn't null, and if it isn't, sets current quest to the next quest
            currentQuest = quests[currentQuestID];
            //Debug.Log(currentQuest.ToString());
        }
        else
        {
            return;
        }
    }

    public void QuestBeginning()
    {
        if (quests[currentQuestID] != null && currentQuestID == 0)
        {
            currentQuest = quests[currentQuestID];
            DisplayQuest();
        }
    }

    public void NextQuest()
    {
        complete = false;
        if (currentQuestID + 1 <= quests.Length) // Checks if the currentQuestID will be higher when increased than the max index in the array
        {
            currentQuestID++;
            currentQuest = quests[currentQuestID];
            Debug.Log(currentQuest.ToString());
        }
        else
        {
            return;
        }
        DisplayQuest();
    }

    public void EndQuest()
    {
        Debug.Log("Check1");
        if (currentQuest.item != null)
        {
            Debug.Log("Check2");
            currentItem.count++; // Add the quest reward (if it exists) to the player's inventory
            InventoryManager.Instance.Add(currentItem);
        }
        dialogueScript.OnQuestComplete();
        InventoryManager.Instance.ListItems();
        NextQuest();
    }

    void DisplayQuest()
    {
        // This code is used to display the current quest in the UI.
        if (currentQuestID == 1)
        {
            foreach (GameObject bush in berrybush)
            {
                bush.SetActive(true);
            }
        }
        Debug.Log(currentQuest.ToString());
        Debug.Log($"{currentQuest.questName}");
        Debug.Log($"{currentQuest.questDescription}");
        questNameText.text = currentQuest.questName;
        questDescriptionText.text = currentQuest.questDescription;
    }
}
