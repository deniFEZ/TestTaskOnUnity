using System.Linq;
using UnityEngine;

public class PlayerSignalManager : MonoBehaviour
{
    [SerializeField] private GameObject TextPrefab;
    [SerializeField] private Transform ParentOfAppearingUI;

    private PlayerDynamic playerDynamicInfo;
    private GameObject playerObject;

    private void PlayerGetHealed_SignalCatched(PlayerGetHealed_Signal signal)
    {
        ShowHeal(signal);
        if (playerObject.GetComponent<PlayerDynamic>().CurrentHP > playerObject.GetComponent<PlayerStaticInformationHolder>().playerStaticInfo.MaxHP)
        {
            playerObject.GetComponent<PlayerDynamic>().CurrentHP = playerObject.GetComponent<PlayerStaticInformationHolder>().playerStaticInfo.MaxHP;
        }
    }
    private void ShowHeal(PlayerGetHealed_Signal signal)
    {
        ItemStatic.TypesOfItem typeOfAttack = signal.itemObject.GetComponent<ItemsStaticInformationHolder>().Item.Type;
        GameObject textObject = Instantiate(TextPrefab, playerObject.transform);
        textObject.transform.SetParent(ParentOfAppearingUI);
        textObject.GetComponent<HealTextMoving>().Initialization(signal.heal);
    }

    private void PlayerWantsToUseItem_SignalCatched(PlayerWantsToUseItem_Signal signal)
    {
        ItemStatic itemStaticInfo = signal.itemObject.GetComponent<ItemsStaticInformationHolder>().Item;
        if (signal.itemObject.GetComponent<ItemDynamic>().TimerForAttackCooldown >= itemStaticInfo.AttackSpeed)
        {
            Player_InventoryUsage.SearchToEnemyAndActivateItem(itemStaticInfo, signal.itemObject);
}
    }

    private void Awake()
    {
        SubscribeToEventbusEvents();
        playerObject = GameObject.FindWithTag("Player");
        playerDynamicInfo = playerObject.GetComponent<PlayerDynamic>();
    }
    private void SubscribeToEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Subscribe<PlayerGetHealed_Signal>(PlayerGetHealed_SignalCatched);
            Eventbus.Instance.Subscribe<PlayerWantsToUseItem_Signal>(PlayerWantsToUseItem_SignalCatched);
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
            Eventbus.Instance.Unsubscribe<PlayerGetHealed_Signal>(PlayerGetHealed_SignalCatched);
            Eventbus.Instance.Unsubscribe<PlayerWantsToUseItem_Signal>(PlayerWantsToUseItem_SignalCatched);
        }
    }
}
