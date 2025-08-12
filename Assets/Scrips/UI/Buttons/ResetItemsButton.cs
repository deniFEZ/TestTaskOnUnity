using UnityEngine;
using UnityEngine.UI;

public class ResetItemsButton : MonoBehaviour
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
        Eventbus.Instance.Publish<ResetRandomItems_Signal>(new ResetRandomItems_Signal());
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
