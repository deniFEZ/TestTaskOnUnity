using System.Collections.Generic;
using UnityEngine;

public class RandomChooseField : MonoBehaviour
{
    public static RandomChooseField Instance { get; private set; }
    public List<GameObject> ListOfItems { get; private set; }

    [Header("Attributes For Spawning Random Items")]

    [SerializeField] private Transform parentForRandomObjectsChooseField;
    [SerializeField] private int amountOfItemsToSpawn;
    [SerializeField] private List<GameObject> listOfItemsToSpawn;
    public Transform ParentForRandomObjectsChooseField => parentForRandomObjectsChooseField;
    public int AmountOfItemsToSpawn => amountOfItemsToSpawn;
    public List<GameObject> ListOfItemsToSpawn => listOfItemsToSpawn;

    public void AddItemToChooseFieldList(GameObject itemObject)
    {
        ListOfItems.Add(itemObject);
    }
    public void DeleteItemWithID(int itemID)
    {
        ListOfItems.RemoveAll(item => item != null && item.GetInstanceID() == itemID);
    }

    private void Awake()
    {
        CheckSignleton();
        InitializeListOfItem();
        SpawnRandomItemsInRandomChooseField.CallCommand_ClearAndSpawnRandomItemsToGrid();
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
    private void InitializeListOfItem()
    {
        ListOfItems = new List<GameObject>();
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
}
