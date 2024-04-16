using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item" ,menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    // Scriptable object for items with the following variables
    [Header("Attrributes")]
    public int itemID;
    public string itemName;
    public Sprite itemSprite;

    [HideInInspector]
    public int count;

}
