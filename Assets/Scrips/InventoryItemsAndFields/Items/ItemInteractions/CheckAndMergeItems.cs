using UnityEngine;

public class CheckAndMergeItems : MonoBehaviour
{
    public static GameObject CallCommand_CheckAndMergeItems(GameObject firstItem, GameObject secondItem)
    {
        return TryToCheckAndMergeItems(firstItem, secondItem);
    }
    private static GameObject TryToCheckAndMergeItems(GameObject firstItem, GameObject secondItem)
    {
        ItemStatic firstItemInfo = firstItem.GetComponent<ItemsStaticInformationHolder>().Item;
        ItemStatic secondItemInfo = secondItem.GetComponent<ItemsStaticInformationHolder>().Item;
        if (CheckItems(firstItemInfo, secondItemInfo))
        {
            return MergeItems(firstItemInfo);
        }
        else
        {
            return null;
        }
    }
    private static bool CheckItems(ItemStatic firstItem, ItemStatic secondItem)
    {
        return (firstItem.name == secondItem.name && firstItem.NextLevelItem != null && firstItem.Level == secondItem.Level);
    }
    private static GameObject MergeItems(ItemStatic itemObject)
    {
        return Instantiate(itemObject.NextLevelItem);
    }
}
