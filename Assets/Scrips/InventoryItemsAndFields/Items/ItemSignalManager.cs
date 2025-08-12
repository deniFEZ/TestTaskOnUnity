using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class ItemSignalManager : MonoBehaviour
{
    public static ItemSignalManager Instance {  get; private set; }
    [SerializeField] public Transform ParentOfItemsForInventoryField;
    [SerializeField] public Transform ParentOfItemsForChooseField;
    [SerializeField] public Transform ParentOfItemsForTemporaryField;
    private void Awake()
    {
        CheckSignleton();
        SubscribeToEventbusEvents();
    }
    private void CheckSignleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void SubscribeToEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Subscribe<MouseStartToDragItemFromInventory_Signal>(MouseDraggingItemFromInventory_SignalCatched);
            Eventbus.Instance.Subscribe<ItemAddedToTheInventory_Signal>(ItemAddedToTheInventory_SignalCatched);
            Eventbus.Instance.Subscribe<MouseButtonReleasedOnInventorySlot_Signal>(MouseButtonReleasedOnInventorySlot_SignalCatched);
            Eventbus.Instance.Subscribe<MouseStartToDragItem_Signal>(MouseStartToDragItem_SignalCatched);
            Eventbus.Instance.Subscribe<ItemCannotBeAddedToTheInventory_Signal>(ItemCannotBeAddedToTheInventory_SignalCatched);
            Eventbus.Instance.Subscribe<TryToMergeItems_Signal>(TryToMergeItems_SignalCatched);
            Eventbus.Instance.Subscribe<MouseButtonReleasedOnChooseField_Signal>(MouseButtonReleasedOnChooseField_SignalCatched);
        }
    }
    private void OnDestroy()
    {
        DestroyInstance();
        UnsubscribeFromEventbusEvents();
    }
    private void DestroyInstance()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void UnsubscribeFromEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Unsubscribe<MouseStartToDragItemFromInventory_Signal>(MouseDraggingItemFromInventory_SignalCatched);
            Eventbus.Instance.Unsubscribe<ItemAddedToTheInventory_Signal>(ItemAddedToTheInventory_SignalCatched);
            Eventbus.Instance.Unsubscribe<MouseButtonReleasedOnInventorySlot_Signal>(MouseButtonReleasedOnInventorySlot_SignalCatched);
            Eventbus.Instance.Unsubscribe<MouseStartToDragItem_Signal>(MouseStartToDragItem_SignalCatched);
            Eventbus.Instance.Unsubscribe<ItemCannotBeAddedToTheInventory_Signal>(ItemCannotBeAddedToTheInventory_SignalCatched);
            Eventbus.Instance.Unsubscribe<TryToMergeItems_Signal>(TryToMergeItems_SignalCatched);
            Eventbus.Instance.Unsubscribe<MouseButtonReleasedOnChooseField_Signal>(MouseButtonReleasedOnChooseField_SignalCatched);
        }
    }

    private void MouseDraggingItemFromInventory_SignalCatched(MouseStartToDragItemFromInventory_Signal signal)
    {
        RemoveItemFromInventory.CallCommand_RemoveItemFromInventory(signal);
        SetItemToTemporaryField.CallCommand_SetItemToTheTemporaryField(signal.itemObject, ParentOfItemsForTemporaryField);
    }
    private void MouseStartToDragItem_SignalCatched(MouseStartToDragItem_Signal signal)
    {
        RandomChooseField.Instance.DeleteItemWithID(signal.itemObject.GetInstanceID());
        SetItemToTemporaryField.CallCommand_SetItemToTheTemporaryField(signal.itemObject, ParentOfItemsForTemporaryField);
    }
    private void ItemAddedToTheInventory_SignalCatched(ItemAddedToTheInventory_Signal signal)
    {
        SnapItemToInventory.CallCommand_SnapItemToInventory(signal, ParentOfItemsForInventoryField);
    }
    private void MouseButtonReleasedOnInventorySlot_SignalCatched(MouseButtonReleasedOnInventorySlot_Signal signal)
    {
        AddItemToInventory.CallCommand_CheckAndAddItemInInventory(signal);
        RandomChooseField.Instance.DeleteItemWithID(signal.itemObject.GetInstanceID());
    }
    private void MouseButtonReleasedOnChooseField_SignalCatched(MouseButtonReleasedOnChooseField_Signal signal)
    {
        SetItemToTheChooseField.CallCommand_ReturnItemToChooseField(signal.itemObject, ParentOfItemsForChooseField);
    }
    private void ItemCannotBeAddedToTheInventory_SignalCatched(ItemCannotBeAddedToTheInventory_Signal signal)
    {
        SetItemToTheChooseField.CallCommand_ReturnItemToChooseField(signal.ItemObject, ParentOfItemsForChooseField);
    }
    private void TryToMergeItems_SignalCatched(TryToMergeItems_Signal signal)
    {
        TryToMergeItems.CallCommand_TryToMergeItems(signal, ParentOfItemsForChooseField, ParentOfItemsForTemporaryField);
    }
}
