using UnityEngine;
using static UnityEditor.Progress;

public class ItemDynamic : MonoBehaviour 
{
    public GameObject InventorySlotObject;
    public float TimerForAttackCooldown;
    public GameObject ItemsBackground;
    public enum AttachmentsStatus { ChooseField, Inventory };
    public AttachmentsStatus AttachmentStatus;

    public void OnEnable()
    {
        InventorySlotObject = null;
        AttachmentStatus = AttachmentsStatus.ChooseField;
        ItemsBackground = this.transform.GetChild(0).gameObject;
    }
}
