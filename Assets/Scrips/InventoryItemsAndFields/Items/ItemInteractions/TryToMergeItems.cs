using Unity.VisualScripting;
using UnityEngine;

public class TryToMergeItems : MonoBehaviour
{
    private static TryToMergeItems_Signal signal;
    private static Transform parentOfItemsForChooseField;
    private static Transform parentOfItemsForTemporaryField;
    public static void CallCommand_TryToMergeItems(TryToMergeItems_Signal signal, Transform ParentOfItemsForChooseField, Transform ParentOfItemsForTemporaryField)
    {
        InitializeVariables(signal, ParentOfItemsForChooseField, ParentOfItemsForTemporaryField);
        Command_TryToMergeItems();
    }
    private static void InitializeVariables(TryToMergeItems_Signal _signal, Transform _ParentOfItemsForChooseField, Transform _ParentOfItemsForTemporaryField)
    {
        signal = _signal;
        parentOfItemsForChooseField = _ParentOfItemsForChooseField;
        parentOfItemsForTemporaryField = _ParentOfItemsForTemporaryField;
    }
    private static void Command_TryToMergeItems()
    {
        GameObject mergedObject = CheckAndMergeItems.CallCommand_CheckAndMergeItems(signal.firstItem, signal.secondItem);
        if (mergedObject != null)
        {
            SetItemsDependingOnTheirAttachments(mergedObject);
        }
        else if (mergedObject == null)
        {
            SetItemsBackToTheirPositions();
        }
    }
    private static void SetItemsDependingOnTheirAttachments(GameObject mergedObject)
    {
        ItemDynamic firstItemDynamicInfo = signal.firstItem.GetComponent<ItemDynamic>();
        ItemDynamic secondItemDynamicInfo = signal.secondItem.GetComponent<ItemDynamic>();
        if (firstItemDynamicInfo.AttachmentStatus == ItemDynamic.AttachmentsStatus.ChooseField && secondItemDynamicInfo.AttachmentStatus == ItemDynamic.AttachmentsStatus.ChooseField)
        {
            DestroyifZeroItemsInInventory(signal.firstItem, signal.secondItem, mergedObject);
        }
        else if (firstItemDynamicInfo.AttachmentStatus == ItemDynamic.AttachmentsStatus.Inventory && secondItemDynamicInfo.AttachmentStatus == ItemDynamic.AttachmentsStatus.ChooseField)
        {
            DestroyifOneItemInInventory(signal.firstItem, signal.secondItem, mergedObject);
        }
        else if (firstItemDynamicInfo.AttachmentStatus == ItemDynamic.AttachmentsStatus.ChooseField && secondItemDynamicInfo.AttachmentStatus == ItemDynamic.AttachmentsStatus.Inventory)
        {
            DestroyifOneItemInInventory(signal.secondItem, signal.firstItem, mergedObject);
        }
        else if (firstItemDynamicInfo.AttachmentStatus == ItemDynamic.AttachmentsStatus.Inventory && secondItemDynamicInfo.AttachmentStatus == ItemDynamic.AttachmentsStatus.Inventory)
        {
            DestroyifTwoItemInInventory(signal.firstItem, signal.secondItem, mergedObject);
        }
    }
    private static void DestroyifZeroItemsInInventory(GameObject firstItem, GameObject secondItem, GameObject mergedObject)
    {
        RandomChooseField.Instance.DeleteItemWithID(firstItem.GetInstanceID());
        RandomChooseField.Instance.DeleteItemWithID(secondItem.GetInstanceID());

        DestroyTwoItems(firstItem, secondItem);

        SetItemToTheChooseField.CallCommand_ReturnItemToChooseField(mergedObject, parentOfItemsForChooseField);
    }
    private static void DestroyifOneItemInInventory(GameObject firstItem, GameObject secondItem, GameObject mergedObject)
    {
        GameObject inventorySlot = firstItem.GetComponent<ItemDynamic>().InventorySlotObject;

        RemoveItemFromInventory.CallCommand_RemoveItemFromInventory(firstItem);
        RandomChooseField.Instance.DeleteItemWithID(secondItem.GetInstanceID());

        DestroyTwoItems(firstItem, secondItem);

        AddItemToInventory.CallCommand_CheckAndAddItemInInventory(PlayersInventory.Instance ,mergedObject, inventorySlot);
    }
    private static void DestroyifTwoItemInInventory(GameObject firstItem, GameObject secondItem, GameObject mergedObject)
    {
        GameObject inventorySlot = firstItem.GetComponent<ItemDynamic>().InventorySlotObject;

        RemoveItemFromInventory.CallCommand_RemoveItemFromInventory(firstItem);
        RemoveItemFromInventory.CallCommand_RemoveItemFromInventory(secondItem);

        DestroyTwoItems(firstItem, secondItem);

        AddItemToInventory.CallCommand_CheckAndAddItemInInventory(PlayersInventory.Instance, mergedObject, inventorySlot);

    }
    private static void DestroyTwoItems(GameObject firstItem, GameObject secondItem)
    {
        Destroy(firstItem);
        Destroy(secondItem);
    }
    private static void SetItemsBackToTheirPositions()
    {
        ItemDynamic secondItemDynamicInfo = signal.secondItem.GetComponent<ItemDynamic>();
        SetItemToTheChooseField.CallCommand_ReturnItemToChooseField(signal.firstItem, parentOfItemsForChooseField);
        if (secondItemDynamicInfo.AttachmentStatus == ItemDynamic.AttachmentsStatus.ChooseField)
        {
            SetItemToTheChooseField.CallCommand_ReturnItemToChooseField(signal.secondItem, parentOfItemsForChooseField);
        }
    }
}
