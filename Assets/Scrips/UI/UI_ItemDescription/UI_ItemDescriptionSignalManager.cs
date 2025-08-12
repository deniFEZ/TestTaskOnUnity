using UnityEngine;

public class UI_ItemDescriptionSignalManager : MonoBehaviour
{
    private void Awake()
    {
        SubscribeToEventbusEvents();
        UI_ItemDescription_ShowHide.CallCommand_Hide_UIBlockOfItemDescription();
    }
    private void SubscribeToEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Subscribe<MouseButtonDown_Signal>(MouseButtonDown_SignalCatched);
            Eventbus.Instance.Subscribe<MouseExitItem_Signal>(MouseExitItem_SignalCatched);
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
            Eventbus.Instance.Unsubscribe<MouseButtonDown_Signal>(MouseButtonDown_SignalCatched);
            Eventbus.Instance.Unsubscribe<MouseExitItem_Signal>(MouseExitItem_SignalCatched);
        }
    }
    private void MouseButtonDown_SignalCatched(MouseButtonDown_Signal signal)
    {
        UI_ItemDescription.Instance.SetAllInformationFromItemForDescription_UI(signal.itemObject);
        UI_ItemDescription_ShowHide.CallCommand_Show_UIBlockOfItemDescription();
    }
    private void MouseExitItem_SignalCatched(MouseExitItem_Signal signal)
    {
        UI_ItemDescription_ShowHide.CallCommand_Hide_UIBlockOfItemDescription();
    }
}
