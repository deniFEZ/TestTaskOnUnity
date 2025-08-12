using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player_InventoryUsage : MonoBehaviour
{
    private static GameObject playerObject;
    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        foreach (GameObject itemObject in PlayersInventory.Instance.Inventory)
        {
            ItemStatic itemStaticInfo = itemObject.GetComponent<ItemsStaticInformationHolder>().Item;
            ItemDynamic itemDynamicInfo = itemObject.GetComponent<ItemDynamic>();

            float maxTimeOfItem = itemStaticInfo.AttackSpeed;
            itemDynamicInfo.TimerForAttackCooldown += Time.deltaTime;
            SetReloadUIFillForItem(itemObject);
            if (itemDynamicInfo.TimerForAttackCooldown >= maxTimeOfItem)
            {
                if (itemStaticInfo.FormOfActivation == ItemStatic.FormsOfActivation.Automated)
                {
                    SearchToEnemyAndActivateItem(itemStaticInfo, itemObject);
                }
            }
        }
    }
    public static void SearchToEnemyAndActivateItem(ItemStatic itemStaticInfo, GameObject itemObject)
    {
        if (itemStaticInfo.Type == ItemStatic.TypesOfItem.Attacking)
        {
            GameObject enemyObject = GetNearestEnemy();
            if (enemyObject != null)
            {
                itemObject.GetComponent<ItemDynamic>().TimerForAttackCooldown = 0;
                ActivateItemAttack(itemObject, enemyObject);

            }
            else
            {
                //Wait for the enemy
            }
        }
        else if (itemStaticInfo.Type == ItemStatic.TypesOfItem.Healing)
        {
            itemObject.GetComponent<ItemDynamic>().TimerForAttackCooldown = 0;
            ActivateItemHealing(itemObject);
        }
    }
    private static GameObject GetNearestEnemy()
    {
        GameObject enemyObject = null;
        float minimalDistance = float.MaxValue;
        float distance;
        foreach (GameObject enemy in EnemiesField.Instance.ListOfEnemies)
        {
            distance = Vector2.Distance(playerObject.transform.position, enemy.transform.position);
            if (distance < minimalDistance)
            {
                minimalDistance = distance;
                enemyObject = enemy;
            }
        }
        return enemyObject;
    }
    private static void ActivateItemAttack(GameObject itemObject, GameObject enemyObject)
    {
        ItemStatic itemStaticInfo = itemObject.GetComponent<ItemsStaticInformationHolder>().Item;
        Eventbus.Instance.Publish<AttackEnemy_Signal>(new AttackEnemy_Signal(enemyObject, itemObject, itemStaticInfo.Damage));
    }
    private static void ActivateItemHealing(GameObject itemObject)
    {
        ItemStatic itemStaticInfo = itemObject.GetComponent<ItemsStaticInformationHolder>().Item;
        Eventbus.Instance.Publish<PlayerGetHealed_Signal>(new PlayerGetHealed_Signal(itemObject, itemStaticInfo.Damage));
    }

    private void SetReloadUIFillForItem(GameObject itemObject)
    {
        float maxTimeOfItem = itemObject.GetComponent<ItemsStaticInformationHolder>().Item.AttackSpeed;
        float currentTimer = itemObject.GetComponent<ItemDynamic>().TimerForAttackCooldown;
        itemObject.GetComponent<ItemDynamic>().ItemsBackground.GetComponent<Image>().fillAmount = currentTimer / maxTimeOfItem;
    }
}
