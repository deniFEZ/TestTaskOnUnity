using UnityEngine;
using static SceneManager;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }
    public enum SceneStates { MergingItemInInventory, Fighting }
    public SceneStates SceneState { get; private set; }

    [SerializeField] private GameObject AllUIElementsOfMergingScene;
    [SerializeField] private GameObject AllScriptsOfMergingScene;

    [SerializeField] private GameObject AllUIElementsOfFightingScene;
    [SerializeField] private GameObject AllScriptsOfFightingScene;

    private void Awake()
    {
        CheckSignleton();

        CallCommand_StartMergingScene();
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
    public void CallCommand_StartFightingScene()
    {
        HideAndDisableMergingItemScene();
        SceneState = SceneStates.Fighting;
        ShowAndEnableFightingItemScene();
    }
    public void CallCommand_StartMergingScene()
    {
        HideAndDisableFightingItemScene();
        SceneState = SceneStates.MergingItemInInventory;
        ShowAndEnableMergingItemScene();
    }

    private void HideAndDisableMergingItemScene()
    {
        AllUIElementsOfMergingScene.SetActive(false);
        AllScriptsOfMergingScene.SetActive(false);
    }
    private void ShowAndEnableMergingItemScene()
    {
        AllUIElementsOfMergingScene.SetActive(true);
        AllScriptsOfMergingScene.SetActive(true);
    }
    private void HideAndDisableFightingItemScene()
    {
        AllUIElementsOfFightingScene.SetActive(false);
        AllScriptsOfFightingScene.SetActive(false);
    }
    private void ShowAndEnableFightingItemScene()
    {
        AllUIElementsOfFightingScene.SetActive(true);
        AllScriptsOfFightingScene.SetActive(true);
    }
}
