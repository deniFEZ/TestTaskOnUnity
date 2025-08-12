using UnityEngine;

public class ItemCannotBeAddedToTheInventory_Signal
{
    public GameObject ItemObject;
    public GameObject InventorySlot;
    public ItemCannotBeAddedToTheInventory_Signal(GameObject _Item, GameObject _InventorySlot)
    {
        ItemObject = _Item;
        InventorySlot = _InventorySlot;
    }
}
