using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySaveData
{
    public int rows;
    public int columns;
    public List<InventoryItemData> items;
}