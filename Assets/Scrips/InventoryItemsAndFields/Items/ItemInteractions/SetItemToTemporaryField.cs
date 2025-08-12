using UnityEngine;

public class SetItemToTemporaryField : MonoBehaviour
{
    public static void CallCommand_SetItemToTheTemporaryField(GameObject itemToReturn, Transform parentObjectTemporaryField)
    {
        SetItemToTheTemporaryField(itemToReturn, parentObjectTemporaryField);
    }
    private static void SetItemToTheTemporaryField(GameObject itemToReturn, Transform parentObjectTemporaryField)
    {
        itemToReturn.transform.SetParent(parentObjectTemporaryField);
    }
}
