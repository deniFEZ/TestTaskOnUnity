using UnityEngine;

public class InventorySlot
{
    public int Row;
    public int Column;
    public GameObject ItemObject;
    public InventorySlot (int _Row, int _Column)
    {
        Row = _Row;
        Column = _Column;
        ItemObject = null;
    }
    public bool IsFree()
    {
        return ItemObject == null;
    }
}
