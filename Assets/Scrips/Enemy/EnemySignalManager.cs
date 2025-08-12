using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySignalManager : MonoBehaviour
{
    [SerializeField] private GameObject TextPrefab;
    [SerializeField] private Transform ParentOfAppearingUI;

    private AttackEnemy_Signal signal;
    private void AttackEnemy_SignalCatched(AttackEnemy_Signal _signal)
    {
        signal = _signal;

        signal.enemyObject.GetComponent<EnemyDynamic>().CurrentHP -= signal.damage;
        ShowDamage(signal);
        if (signal.enemyObject.GetComponent<EnemyDynamic>().CurrentHP <= 0)
        {
            EnemiesField.Instance.ListOfEnemies.Remove(EnemiesField.Instance.ListOfEnemies.FirstOrDefault(enemy => enemy.GetInstanceID() == signal.enemyObject.GetInstanceID()));
            Destroy(signal.enemyObject);
        }
    }
    private void ShowDamage(AttackEnemy_Signal signal)
    {
        ItemStatic.TypesOfItem typeOfAttack = signal.itemObject.GetComponent<ItemsStaticInformationHolder>().Item.Type;
        GameObject textObject = Instantiate(TextPrefab, signal.enemyObject.transform);
        textObject.transform.SetParent(ParentOfAppearingUI);
        textObject.GetComponent<DamageTextMoving>().Initialization(signal.damage);
    }

    private void Awake()
    {
        SubscribeToEventbusEvents();
    }
    private void SubscribeToEventbusEvents()
    {
        if (Eventbus.Instance != null)
        {
            Eventbus.Instance.Subscribe<AttackEnemy_Signal>(AttackEnemy_SignalCatched);
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
            Eventbus.Instance.Unsubscribe<AttackEnemy_Signal>(AttackEnemy_SignalCatched);
        }
    }
}
