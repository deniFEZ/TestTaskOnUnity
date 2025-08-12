using System.Collections.Generic;
using UnityEngine;

public class EnemiesField : MonoBehaviour
{
    public static EnemiesField Instance {  get; private set; }
    public List<GameObject> ListOfEnemies { get; private set; }

    [SerializeField] GameObject TEST_PrefabEnemy_Mannequin;
    [SerializeField] Transform UI_ParentOfEnemies;
    public void AddEnemy(GameObject enemyObject)
    {
        ListOfEnemies.Add(Instantiate(TEST_PrefabEnemy_Mannequin, UI_ParentOfEnemies));
    }

    private void Awake()
    {
        CheckSignleton();
        InitializeListOfEnemies();
        AddEnemy(TEST_PrefabEnemy_Mannequin); // For Testing
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
    private void InitializeListOfEnemies()
    {
        ListOfEnemies = new List<GameObject>();
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
