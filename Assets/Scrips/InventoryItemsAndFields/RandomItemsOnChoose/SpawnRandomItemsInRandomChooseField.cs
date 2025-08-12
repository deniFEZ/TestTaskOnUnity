using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomItemsInRandomChooseField : MonoBehaviour
{
    public static void CallCommand_ClearAndSpawnRandomItemsToGrid()
    {
        ClearAndSpawnRandomItemsToGrid();
    }
    
    private static void ClearAndSpawnRandomItemsToGrid()
    {
        ClearGrid();
        ClearList();
        AddRandomItems();
    }
    private static void ClearGrid()
    {
        if (RandomChooseField.Instance.ParentForRandomObjectsChooseField.childCount != 0)
        {
            foreach (Transform child in RandomChooseField.Instance.ParentForRandomObjectsChooseField)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
    private static void ClearList()
    {
        RandomChooseField.Instance.ListOfItems.Clear();
    }
    private static void AddRandomItems()
    {
        for (int numberOFItem = 0; numberOFItem < RandomChooseField.Instance.AmountOfItemsToSpawn; numberOFItem++)
        {
            GameObject itemToSpawn = GetRandomLoot();
            if (itemToSpawn != null)
            {
                RandomChooseField.Instance.ListOfItems.Add(Instantiate(itemToSpawn, RandomChooseField.Instance.ParentForRandomObjectsChooseField));
            }
        }
    }
    private static GameObject GetRandomLoot()
    {
        float totalChance = 0;
        foreach (var loot in RandomChooseField.Instance.ListOfItemsToSpawn)
            totalChance += loot.GetComponent<ItemsStaticInformationHolder>().Item.ChanceToBeSpawnedInPercentage;

        float randomValue = Random.Range(0, totalChance);
        float currentSum = 0;

        foreach (var loot in RandomChooseField.Instance.ListOfItemsToSpawn)
        {
            currentSum += loot.GetComponent<ItemsStaticInformationHolder>().Item.ChanceToBeSpawnedInPercentage;
            if (randomValue <= currentSum)
            {
                return loot;
            }
        }
        return null;
    }
}
