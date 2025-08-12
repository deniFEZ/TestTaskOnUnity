using UnityEngine;

public class MouseButtonReleasedOnInventorySlot_Signal
{
    public PlayersInventory playersInventory;
    public GameObject itemObject;
    public GameObject inventorySlot;
    public MouseButtonReleasedOnInventorySlot_Signal(PlayersInventory _playersInventory, GameObject _itemObject, GameObject _inventorySlot)
    {
        itemObject = _itemObject;
        inventorySlot = _inventorySlot;
        playersInventory = _playersInventory;
    }
}
