using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Create New Quest")]
public class Quest : ScriptableObject
{
    // Scriptable object to hold various quests by the variables below
    [Header("Attrributes")]
    public int questID;
    public string questName;
    public string questDescription;
    public bool questComplete;
    public Item item;
}
