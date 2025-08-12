using UnityEngine;
using UnityEngine.UI;

public class SceneSignalManager : MonoBehaviour
{
    private void Awake()
    {
        SubscribeToEventbusEvents();
    }
    private void SubscribeToEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Subscribe<StartMergingScene_Signal>(StartMergingScene_SignalCatched);
            Eventbus.Instance.Subscribe<StartFightingScene_Signal>(StartFightingScene_SignalCatched);
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
            Eventbus.Instance.Unsubscribe<StartMergingScene_Signal>(StartMergingScene_SignalCatched);
            Eventbus.Instance.Unsubscribe<StartFightingScene_Signal>(StartFightingScene_SignalCatched);
        }
    }

    private void StartMergingScene_SignalCatched(StartMergingScene_Signal signal)
    {
        foreach (GameObject itemObject in PlayersInventory.Instance.Inventory)
        {
            float maxTimeOfItem = itemObject.GetComponent<ItemsStaticInformationHolder>().Item.AttackSpeed;
            float currentTimer = maxTimeOfItem;
            itemObject.GetComponent<ItemDynamic>().ItemsBackground.GetComponent<Image>().fillAmount = currentTimer / maxTimeOfItem;
        }
        SceneManager.Instance.CallCommand_StartMergingScene();
    }
    private void StartFightingScene_SignalCatched(StartFightingScene_Signal signal)
    {
        SceneManager.Instance.CallCommand_StartFightingScene();
    }
}
