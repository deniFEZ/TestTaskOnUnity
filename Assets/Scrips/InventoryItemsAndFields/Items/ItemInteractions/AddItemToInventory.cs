using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class AddItemToInventory : MonoBehaviour
{
    private static InventorySlot slotInfo;
    private static ItemStatic itemStaticInfo;
    private static ItemDynamic itemDynamicInfo;
    private static GameObject itemObject;
    private static PlayersInventory playersInventory;
    private static GameObject inventorySlot;

    public static void CallCommand_CheckAndAddItemInInventory(MouseButtonReleasedOnInventorySlot_Signal signal)
    {
        Initialize(signal.playersInventory, signal.itemObject, signal.inventorySlot);
        CheckAndAddItemInInventory();
    }
    public static void CallCommand_CheckAndAddItemInInventory(PlayersInventory playersInventory, GameObject _itemObject, GameObject inventorySlot)
    {
        Initialize(playersInventory, _itemObject, inventorySlot);
        CheckAndAddItemInInventory();
    }
    private static void Initialize(PlayersInventory _playersInventory, GameObject _itemObject, GameObject _inventorySlot)
    {
        inventorySlot = _inventorySlot;
        itemObject = _itemObject;
        playersInventory = _playersInventory;
        slotInfo = inventorySlot.GetComponent<InventorySlotHolder>().InventorySlot;
        itemStaticInfo = itemObject.GetComponent<ItemsStaticInformationHolder>().Item;
        itemDynamicInfo = itemObject.GetComponent<ItemDynamic>();
    }
    private static void CheckAndAddItemInInventory()
    {
        if (CheckSlotsAreValidForItem(slotInfo, itemStaticInfo, itemDynamicInfo))
        {
            AddItemIntoSlotsInventory(slotInfo, itemStaticInfo, itemObject);
            AddItemIntoInventory(itemObject);
            itemDynamicInfo.AttachmentStatus = ItemDynamic.AttachmentsStatus.Inventory;
            itemDynamicInfo.InventorySlotObject = inventorySlot;
            Eventbus.Instance.Publish<ItemAddedToTheInventory_Signal>(new ItemAddedToTheInventory_Signal(itemObject, inventorySlot));
        }
        else  
        {
            Eventbus.Instance.Publish<ItemCannotBeAddedToTheInventory_Signal>(new ItemCannotBeAddedToTheInventory_Signal(itemObject, inventorySlot));
        }
    }
    private static bool CheckSlotsAreValidForItem(InventorySlot slotInfo, ItemStatic itemInfo, ItemDynamic itemDynamicInfo)
    {
        int startRow = slotInfo.Row;
        int startCol = slotInfo.Column;
        int endRow = slotInfo.Row + itemInfo.x;
        int endCol = slotInfo.Column + itemInfo.y;
        if (itemInfo.FormOfWeapon == ItemStatic.FormsOfWeapon.Normal)
        {
            for (int row = startRow; row < endRow; row++)
            {
                for (int col = startCol; col < endCol; col++)
                {
                    if (!CheckIfXOrYInOfBounds(row, col)) return false;
                    if (!CheckIfSlotIsFree(row, col)) return false;
                }
            }
        }
        else if (itemInfo.FormOfWeapon == ItemStatic.FormsOfWeapon.r_Form)
        {
            for (int row = startRow; row < endRow; row++)
            {
                if (!CheckIfXOrYInOfBounds(row, startCol)) return false;
                if (!CheckIfSlotIsFree(row, startCol)) return false;
            }
            for (int col = startCol; col < endCol; col++)
            {
                if (!CheckIfXOrYInOfBounds(startRow, col)) return false;
                if (!CheckIfSlotIsFree(startRow, col)) return false;
            }
        }
        return true;
    }
    private static void AddItemIntoSlotsInventory(InventorySlot slotInfo, ItemStatic itemInfo, GameObject itemObject)
    {
        int startRow = slotInfo.Row;
        int startCol = slotInfo.Column;
        int endRow = slotInfo.Row + itemInfo.x;
        int endCol = slotInfo.Column + itemInfo.y;
        if (itemInfo.FormOfWeapon == ItemStatic.FormsOfWeapon.Normal)
        {
            for (int row = startRow; row < endRow; row++)
            {
                for (int col = startCol; col < endCol; col++)
                {
                    playersInventory.InventoryGrid[row][col].GetComponent<InventorySlotHolder>().InventorySlot.ItemObject = itemObject;
                }
            }
        }
        else if (itemInfo.FormOfWeapon == ItemStatic.FormsOfWeapon.r_Form)
        {
            for (int row = startRow; row < endRow; row++)
            {
                playersInventory.InventoryGrid[row][startCol].GetComponent<InventorySlotHolder>().InventorySlot.ItemObject = itemObject;
            }
            for (int col = startCol; col < endCol; col++)
            {
                playersInventory.InventoryGrid[startRow][col].GetComponent<InventorySlotHolder>().InventorySlot.ItemObject = itemObject;
            }
        }
    }
    private static void AddItemIntoInventory(GameObject itemObject)
    {
        playersInventory.Inventory.Add(itemObject);
    }
    private static bool CheckIfXOrYInOfBounds(int row, int col)
    {
        return ((col >= 0 && col < playersInventory.InventorySlotsColumns) && (row >= 0 && row < playersInventory.InventorySlotsRows));
    }
    private static bool CheckIfSlotIsFree(int row, int col)
    {
        return playersInventory.InventoryGrid[row][col].GetComponent<InventorySlotHolder>().InventorySlot.IsFree();
    }
}
