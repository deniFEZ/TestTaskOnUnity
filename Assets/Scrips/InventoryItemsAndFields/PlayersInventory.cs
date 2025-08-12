using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayersInventory : MonoBehaviour
{
    public static PlayersInventory Instance { get; private set; }
    public List<GameObject> Inventory { get; private set; }
    public List<List<GameObject>> InventoryGrid { get; private set; }

    [Header("InventoryCreation")]
    [SerializeField] private List<GameObject> EveryItem;
    [SerializeField] private Transform InventorySlotsContatiner;
    [SerializeField] private GameObject InventorySlotPrefab;
    [SerializeField] public int InventorySlotsRows;
    [SerializeField] public int InventorySlotsColumns;

    private void Awake()
    {
        CheckSignleton();
        CreateInventorySlotsForInventory();
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
    private void CreateInventorySlotsForInventory()
    {
        Inventory = new List<GameObject>();
        InventoryGrid = new List<List<GameObject>>();
        for (int row = 0; row < InventorySlotsRows; row++)
        {
            List<GameObject> rowList = new List<GameObject>();

            for (int col = 0; col < InventorySlotsColumns; col++)
            {
                GameObject slot = Instantiate(InventorySlotPrefab, InventorySlotsContatiner);
                slot.GetComponent<InventorySlotHolder>().InventorySlot = new InventorySlot(row, col);
                rowList.Add(slot);
            }
            InventoryGrid.Add(rowList);
        }
    }
    private void ClearInventoryItemssAndSendThemToChooseField()
    {
        var itemsCopy = new List<GameObject>(Inventory);

        foreach (var itemObject in itemsCopy)
        {
            SetItemToTheChooseField.CallCommand_ReturnItemToChooseField(itemObject, ItemSignalManager.Instance.ParentOfItemsForChooseField);
            RemoveItemFromInventory.CallCommand_RemoveItemFromInventory(itemObject);
        }

        if (InventoryGrid == null || InventoryGrid.Count == 0) return;

        Inventory.Clear();

        for (int row = 0; row < InventoryGrid.Count; row++)
        {
            for (int col = 0; col < InventoryGrid[row].Count; col++)
            {
                Destroy(InventoryGrid[row][col]);
            }
        }
        InventoryGrid.Clear();
    }
    public void AddRowInInventory()
    {
        ClearInventoryItemssAndSendThemToChooseField();

        InventorySlotsRows += 1;

        CreateInventorySlotsForInventory();
    }
    public void AddColumnInInventory()
    {
        ClearInventoryItemssAndSendThemToChooseField();

        InventorySlotsColumns += 1;
        InventorySlotsContatiner.GetComponent<GridLayoutGroup>().constraintCount += 1;

        CreateInventorySlotsForInventory();
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

    private void OnApplicationQuit()
    {
        SaveInventory();
    }
    private void Start()
    {
        LoadInventory();
    }
    private void SaveInventory()
    {
        InventorySaveData saveData = new InventorySaveData();
        saveData.rows = InventorySlotsRows;
        saveData.columns = InventorySlotsColumns;
        saveData.items = new List<InventoryItemData>();

        foreach (var itemObj in Inventory)
        {
            var dynamicInfo = itemObj.GetComponent<ItemDynamic>();
            var staticInfo = itemObj.GetComponent<ItemsStaticInformationHolder>().Item;

            var slot = dynamicInfo.InventorySlotObject.GetComponent<InventorySlotHolder>().InventorySlot;

            InventoryItemData data = new InventoryItemData();
            data.itemID = staticInfo.ID;
            data.row = slot.Row;
            data.column = slot.Column;

            saveData.items.Add(data);
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("PlayerInventory", json);
        PlayerPrefs.Save();
    }

    private void LoadInventory()
    {
        //PlayerPrefs.DeleteKey("PlayerInventory"); if you want to delete your items save
        if (!PlayerPrefs.HasKey("PlayerInventory")) return;

        string json = PlayerPrefs.GetString("PlayerInventory");
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);

        while (InventorySlotsRows != saveData.rows)
        {
            AddRowInInventory();
        }
        while (InventorySlotsColumns != saveData.columns)
        {
            AddColumnInInventory();
        }

        ClearInventoryItemssAndSendThemToChooseField();
        CreateInventorySlotsForInventory();

        foreach (var itemData in saveData.items)
        {
            GameObject itemPrefab = GetItemPrefabByID(itemData.itemID);
            if (itemPrefab != null)
            {
                GameObject itemObj = Instantiate(itemPrefab);
                GameObject slotObj = InventoryGrid[itemData.row][itemData.column];
                AddItemToInventory.CallCommand_CheckAndAddItemInInventory(this, itemObj, slotObj);
            }
        }
    }

    private GameObject GetItemPrefabByID(int id)
    {
        foreach (GameObject item in EveryItem)
        {
            if (item.GetComponent<ItemsStaticInformationHolder>().Item.ID == id)
            {
                return item;
            }
        }
        return null; 
    }
}
