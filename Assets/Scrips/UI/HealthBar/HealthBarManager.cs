using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private Image healthBar_HP;

    private void HPChanged_SignalCatched(HPChanged_Signal signal)
    {
        healthBar_HP.fillAmount = signal.currentHP / 100;
    }

    private void Awake()
    {
        SubscribeToEventbusEvents();
    }
    private void SubscribeToEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Subscribe<HPChanged_Signal>(HPChanged_SignalCatched);
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
            Eventbus.Instance.Unsubscribe<HPChanged_Signal>(HPChanged_SignalCatched);
        }
    }
}
