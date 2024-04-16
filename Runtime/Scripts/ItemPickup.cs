using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool pickingUp = false;

    private void Start()
    {
        item.count = 0;
    }

    public void Pickup()
    {
        item.count++; // Increases item count when colliding with an object that holds this script, then destroys the game object so that it cannot be collided with again
        if (item.count <= 1)
        {
            InventoryManager.Instance.Add(item);
            InventoryManager.Instance.ListItems();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
            InventoryManager.Instance.ListItems();
        }
        Debug.Log($"{QuestManager.Instance.currentQuestID == 1} + {item == InventoryManager.Instance.items[0]} + {item.count >= 3}");
        
        if (QuestManager.Instance.currentQuestID == 1 && item == InventoryManager.Instance.items[0] && item.count >= 3) // This checks if all the conditions to proceed to the next quest have been fulfilled
        {
            Debug.Log("Quest should be complete");
            QuestManager.Instance.currentQuest.questComplete = true;
            QuestManager.Instance.complete = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pickingUp)
        {
            pickingUp = true;
            Pickup();
        }
    }
}
