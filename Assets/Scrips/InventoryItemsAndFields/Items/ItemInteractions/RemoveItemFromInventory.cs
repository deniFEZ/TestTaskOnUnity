using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class RemoveItemFromInventory : MonoBehaviour
{
    public static void CallCommand_RemoveItemFromInventory(MouseStartToDragItemFromInventory_Signal signal)
    {
        RemoveItemFromInventoriesSlots(signal.itemObject);
    }
    public static void CallCommand_RemoveItemFromInventory(GameObject itemToDelete)
    {
        RemoveItemFromInventoriesSlots(itemToDelete);
    }
    private static void RemoveItemFromInventoriesSlots(GameObject itemToDelete)
    {
        RemoveItemFromInventoriesGrid(itemToDelete.GetInstanceID());
        RemoveItemFromInventoryList(itemToDelete.GetInstanceID());
        itemToDelete.GetComponent<ItemDynamic>().InventorySlotObject = null;
        itemToDelete.GetComponent<ItemDynamic>().AttachmentStatus = ItemDynamic.AttachmentsStatus.ChooseField;

    }
    private static void RemoveItemFromInventoriesGrid(int itemID)
    {
        int inventoryRaws = PlayersInventory.Instance.InventorySlotsRows;
        int inventoryCol = PlayersInventory.Instance.InventorySlotsColumns;
        for (int row = 0; row < inventoryRaws; row++)
        {
            for (int col = 0; col < inventoryCol; col++)
            {
                var slot = PlayersInventory.Instance.InventoryGrid[row][col].GetComponent<InventorySlotHolder>().InventorySlot;
                if (slot.ItemObject != null && slot.ItemObject.GetInstanceID() == itemID)
                {
                    slot.ItemObject = null;
                }
            }
        }
    }
    private static void RemoveItemFromInventoryList(int itemID)
    {
        PlayersInventory.Instance.Inventory.RemoveAll(item => item != null && item.GetInstanceID() == itemID);
    }
}
