using UnityEngine;

public class InventorySignalManager : MonoBehaviour
{
    private void Awake()
    {
        SubscribeToEventbusEvents();
    }
    private void SubscribeToEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Subscribe<AddColumnToTheInventory_Signal>(AddColumnToTheInventory_SignalCatched);
            Eventbus.Instance.Subscribe<AddRowToTheInventory_Signal>(AddRawsToTheInventory_SignalCatched);
        }
    }
    private void OnDestroy()
    {
        UnsubscribeFromEventbusEvents();
    }
    private void UnsubscribeFromEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Unsubscribe<AddColumnToTheInventory_Signal>(AddColumnToTheInventory_SignalCatched);
            Eventbus.Instance.Unsubscribe<AddRowToTheInventory_Signal>(AddRawsToTheInventory_SignalCatched);
        }
    }

    private void AddColumnToTheInventory_SignalCatched(AddColumnToTheInventory_Signal signal)
    {
        PlayersInventory.Instance.AddColumnInInventory();
    }
    private void AddRawsToTheInventory_SignalCatched(AddRowToTheInventory_Signal signal)
    {
        PlayersInventory.Instance.AddRowInInventory();
    }
}
