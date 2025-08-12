using TMPro;
using UnityEngine;

public class UpdateStateOfPanels : MonoBehaviour
{
    [SerializeField] Transform PanelWithSlots;
    private void Update()
    {
        if (PanelWithSlots.childCount != 0)
        {
            foreach (Transform child in PanelWithSlots)
            {
                GameObject childObj = child.gameObject;
                InventorySlot slotInfo = childObj.GetComponent<InventorySlotHolder>().InventorySlot;
                TextMeshProUGUI textObj = child.GetChild(0).GetComponent<TextMeshProUGUI>();

                textObj.text = slotInfo.Row + " " + slotInfo.Column;
                /*
                if (slotInfo.ItemObject == null)
                {
                    textObj.text = "";
                }
                else
                {
                    textObj.text = slotInfo.Row + " " + slotInfo.Column;
                }
                */
            }
        }
    }
}
