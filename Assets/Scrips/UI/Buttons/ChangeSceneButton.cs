using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
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
        if (SceneManager.Instance.SceneState == SceneManager.SceneStates.Fighting)
        {
            Eventbus.Instance.Publish<StartMergingScene_Signal>(new StartMergingScene_Signal());
        }
        else if (SceneManager.Instance.SceneState == SceneManager.SceneStates.MergingItemInInventory)
        {
            Eventbus.Instance.Publish<StartFightingScene_Signal>(new StartFightingScene_Signal());
        }
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
