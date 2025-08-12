using UnityEngine;
using UnityEngine.UI;

public class AddRowToTheInventoryButton : MonoBehaviour
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
        Eventbus.Instance.Publish<AddRowToTheInventory_Signal>(new AddRowToTheInventory_Signal());
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
