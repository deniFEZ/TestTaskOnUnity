using UnityEngine;


public class SetItemToTheChooseField : MonoBehaviour
{
    public static void CallCommand_ReturnItemToChooseField(GameObject itemToReturn, Transform parentForRandomObjects)
    {
        ReturnItemToChooseField(itemToReturn, parentForRandomObjects);
    }
    private static void ReturnItemToChooseField(GameObject itemToSet, Transform parentForRandomObjects)
    {
        itemToSet.transform.SetParent(parentForRandomObjects);
        itemToSet.transform.SetSiblingIndex(0);

        RandomChooseField.Instance.AddItemToChooseFieldList(itemToSet);
    }
}
