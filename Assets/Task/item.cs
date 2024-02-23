using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class item : ScriptableObject
{ 
    public string itemName;
    public Sprite ItemImage;
    public int itemHold;
    [TextArea]
    public string itemInformation;

}

