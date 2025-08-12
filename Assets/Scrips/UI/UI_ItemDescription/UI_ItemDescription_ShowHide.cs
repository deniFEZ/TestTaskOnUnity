using UnityEngine;

public class UI_ItemDescription_ShowHide : MonoBehaviour
{
    public static void CallCommand_Show_UIBlockOfItemDescription()
    {
        Command_Show_UIBlockOfItemDescription();
    }
    private static void Command_Show_UIBlockOfItemDescription()
    {
        UI_ItemDescription.Instance.UI_BlockOfDescriptionOfItem.SetActive(true);
    }
    public static void CallCommand_Hide_UIBlockOfItemDescription()
    {
        Command_Hide_UIBlockOfItemDescription();
    }
    private static void Command_Hide_UIBlockOfItemDescription()
    {
        UI_ItemDescription.Instance.UI_BlockOfDescriptionOfItem.SetActive(false);
    }
}
