using UnityEngine;

public class ItemAddedToTheInventory_Signal
{
    public GameObject Item;
    public GameObject InventorySlot;
    public ItemAddedToTheInventory_Signal(GameObject _Item, GameObject _InventorySlot)
    {
        Item = _Item;
        InventorySlot = _InventorySlot;
    }
}
