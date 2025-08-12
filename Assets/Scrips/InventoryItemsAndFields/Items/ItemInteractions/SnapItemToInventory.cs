using UnityEngine;

public class SnapItemToInventory : MonoBehaviour
{
    public static void CallCommand_SnapItemToInventory(ItemAddedToTheInventory_Signal signal, Transform originalParentOfItem)
    {
        SnapItemToTheGridSlot(signal, originalParentOfItem);
    }
    private static void SnapItemToTheGridSlot(ItemAddedToTheInventory_Signal signal, Transform originalParentOfItem)
    {
        RectTransform itemRectTransform = signal.Item.GetComponent<RectTransform>();

        signal.Item.transform.SetParent(signal.InventorySlot.transform);

        itemRectTransform.localPosition = Vector3.zero;

        signal.Item.transform.SetParent(originalParentOfItem);
    }
}
