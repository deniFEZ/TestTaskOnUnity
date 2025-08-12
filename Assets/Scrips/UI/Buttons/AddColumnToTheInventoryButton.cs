using UnityEngine;
using UnityEngine.UI;

public class AddColumnToTheInventoryButton : MonoBehaviour
{
    [SerializeField] private Button myButton;

    private void Awake()
    {
        SubscribeToEvents();
    }
    private void SubscribeToEvents()
    {
        myButton.onClick.AddListener(SendSignalThatButtonClicked);
    }
    private void SendSignalThatButtonClicked()
    {
        Eventbus.Instance.Publish<AddColumnToTheInventory_Signal>(new AddColumnToTheInventory_Signal());
    }
    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    private void UnsubscribeFromEvents()
    {
        myButton.onClick.RemoveListener(SendSignalThatButtonClicked);
    }
}
