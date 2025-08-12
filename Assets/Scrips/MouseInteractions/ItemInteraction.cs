using UnityEngine;

public class ItemInteraction : MonoBehaviour
{

    void FixedUpdate()
    {
        if (MouseInputManager.Instance != null)
        {
            if (CheckIfDraggingItem())
            {
                MoveItemWithAMouse();
            }
        }
    }

    private bool CheckIfDraggingItem()
    {
        return MouseInputManager.Instance.MouseInputState == MouseInputManager.StatesOfMouseInput.DraggingItem;
    }
    private void MoveItemWithAMouse()
    {
        if (MouseInputManager.Instance.CurrentItem == null) return;
        RectTransform itemRect = MouseInputManager.Instance.CurrentItem.GetComponent<RectTransform>();
        if (itemRect == null) return;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            itemRect.parent as RectTransform,
            Input.mousePosition,
            null,
            out localPoint);

        itemRect.anchoredPosition = localPoint;
    }
}
