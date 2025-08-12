using UnityEngine;

public class RandomChooseFieldSignalManager : MonoBehaviour
{
    private void Awake()
    {
        SubscribeToEventbusEvents();
    }
    private void SubscribeToEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Subscribe<ResetRandomItems_Signal>(ResetRandomItems_SignalCatched);
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
            Eventbus.Instance.Unsubscribe<ResetRandomItems_Signal>(ResetRandomItems_SignalCatched);
        }
    }

    private void ResetRandomItems_SignalCatched(ResetRandomItems_Signal signal)
    {
        SpawnRandomItemsInRandomChooseField.CallCommand_ClearAndSpawnRandomItemsToGrid();
    }
}
