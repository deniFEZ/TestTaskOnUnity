using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemDescription : MonoBehaviour
{
    public static UI_ItemDescription Instance {  get; private set; }
    [SerializeField] private GameObject UI_blockOfDescriptionOfItem;
    public GameObject UI_BlockOfDescriptionOfItem => UI_blockOfDescriptionOfItem;

    [Header("Objects To Fill")]
    [SerializeField] private Image ItemImage;
    [SerializeField] private TextMeshProUGUI ItemObjectNameText;
    [SerializeField] private TextMeshProUGUI ItemDescriptionText;
    [SerializeField] private TextMeshProUGUI ItemLevelText;
    [SerializeField] private TextMeshProUGUI ItemTypeText;
    [SerializeField] private TextMeshProUGUI ItemDamageText;
    [SerializeField] private TextMeshProUGUI ItemFormOfActivation;

    private void Awake()
    {
        CheckSignleton();
    }
    private void CheckSignleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        DestroyInstance();
    }
    private void DestroyInstance()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void SetAllInformationFromItemForDescription_UI(GameObject itemObject)
    {
        ItemStatic itemStaticInformation = itemObject.GetComponent<ItemsStaticInformationHolder>().Item;

        ItemImage.GetComponent<Image>().sprite = itemObject.GetComponent<Image>().sprite;
        ItemObjectNameText.text = itemStaticInformation.NameItem;
        ItemDescriptionText.text = itemStaticInformation.DescriptionItem;
        ItemLevelText.text = GetTextToLevelText(itemStaticInformation);
        ItemTypeText.text = GetTextToItemText(itemStaticInformation);
        ItemDamageText.text = itemStaticInformation.Damage.ToString();
        ItemFormOfActivation.text = GetFormOfActivationText(itemStaticInformation);

    }
    private string GetTextToLevelText(ItemStatic itemStaticInformation)
    {
        string textToReturn = "";
        if (itemStaticInformation.NextLevelItem == null) textToReturn = "Max: " + itemStaticInformation.Level;
        else textToReturn = itemStaticInformation.Level.ToString();
        return textToReturn;
    }
    private string GetTextToItemText(ItemStatic itemStaticInformation)
    {
        string textToReturn = "";
        if (itemStaticInformation.Type == ItemStatic.TypesOfItem.Healing) textToReturn = "Healing";
        else if (itemStaticInformation.Type == ItemStatic.TypesOfItem.Attacking) textToReturn = "Damage";
        return textToReturn;
    }
    private string GetFormOfActivationText(ItemStatic itemStaticInformation)
    {
        string textToReturn = "";
        if (itemStaticInformation.FormOfActivation == ItemStatic.FormsOfActivation.Manually) textToReturn = "Manually";
        else if (itemStaticInformation.FormOfActivation == ItemStatic.FormsOfActivation.Automated) textToReturn = "Automated";
        return textToReturn;
    }
}
