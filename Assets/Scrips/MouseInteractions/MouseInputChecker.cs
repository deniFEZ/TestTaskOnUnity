using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseInputChecker : MonoBehaviour
{

    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    private PointerEventData pointerEventData;
    private List<RaycastResult> results;

    private void Awake()
    {
        FindSerializeFieldObjectIfTheyNull();
    }
    private void FindSerializeFieldObjectIfTheyNull()
    {
        if (eventSystem == null)
            eventSystem = FindFirstObjectByType<EventSystem>();
        if (raycaster == null)
            raycaster = FindFirstObjectByType<GraphicRaycaster>();
    }

    private void Update()
    {
        CreateResultsList();

        if (SceneManager.Instance.SceneState == SceneManager.SceneStates.MergingItemInInventory)
        {
            CheckResultsListOnHitsAndMouseInMergingState();
        }
        if (SceneManager.Instance.SceneState == SceneManager.SceneStates.Fighting)
        {
            CheckResultsListOnHitsAndMouseInFightingState();
        }
    }
    private void CreateResultsList()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);
    }
    private void CheckResultsListOnHitsAndMouseInMergingState()
    {
        GameObject foundfirstItemObject = null;
        GameObject foundsecondItemObject = null;
        GameObject selectedInventorySlot = null;

        foreach (RaycastResult result in results)
        {
            if (CheckObjectIsItem(result) && foundfirstItemObject == null)
            {
                foundfirstItemObject = result.gameObject;
            }
            else if (CheckObjectIsItem(result) && foundfirstItemObject != null && foundsecondItemObject == null)
            {
                foundsecondItemObject = result.gameObject;
            }
            else if (CheckObjectIsInventorySlot(result))
            {
                selectedInventorySlot = result.gameObject;
            }
        }

        CheckIfThereIsSomethingUnderMouseAndActionOfThePlayer(foundfirstItemObject, foundsecondItemObject, selectedInventorySlot);

    }
    private void CheckIfThereIsSomethingUnderMouseAndActionOfThePlayer(GameObject foundfirstItemObject, GameObject foundsecondItemObject, GameObject selectedInventorySlot)
    {
        if (foundfirstItemObject != null)
        {
            ThereIsItemUnderMouse(foundfirstItemObject, foundsecondItemObject, selectedInventorySlot);
        }
        else
        {
            ThereIsNoItemUnderMouse();
        }
    }
    private void ThereIsItemUnderMouse(GameObject foundfirstItemObject, GameObject foundsecondItemObject, GameObject selectedInventorySlot)
    {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0))
        {
            PlayerDidntClickMouse(foundfirstItemObject);
        }
        else if (Input.GetMouseButton(0))
        {
            PlayerHoldsMouseButton(foundfirstItemObject);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PlayerReleasedMouseButton(foundfirstItemObject, foundsecondItemObject, selectedInventorySlot);
        }
    }
    private void PlayerDidntClickMouse(GameObject foundfirstItemObject)
    {
        if (MouseInputManager.Instance.CurrentItem == null)
        {
            Eventbus.Instance.Publish<MouseEnterItem_Signal>(new MouseEnterItem_Signal(foundfirstItemObject));
        }
    }
    private void PlayerHoldsMouseButton(GameObject foundfirstItemObject)
    {
        PlayerClickedMouseButton(foundfirstItemObject);
        if (MouseInputManager.Instance.MouseInputState == MouseInputManager.StatesOfMouseInput.OnItem)
        {
            if (foundfirstItemObject.GetComponent<ItemDynamic>().AttachmentStatus == ItemDynamic.AttachmentsStatus.Inventory)
            {
                Eventbus.Instance.Publish<MouseStartToDragItemFromInventory_Signal>(new MouseStartToDragItemFromInventory_Signal(foundfirstItemObject));
            }
            else if (foundfirstItemObject.GetComponent<ItemDynamic>().AttachmentStatus == ItemDynamic.AttachmentsStatus.ChooseField)
            {
                Eventbus.Instance.Publish<MouseStartToDragItem_Signal>(new MouseStartToDragItem_Signal(foundfirstItemObject));
            }
        }
    }
    private void PlayerClickedMouseButton(GameObject foundfirstItemObject)
    {
        Eventbus.Instance.Publish<MouseButtonDown_Signal>(new MouseButtonDown_Signal(foundfirstItemObject));
    }
    private void PlayerReleasedMouseButton(GameObject foundfirstItemObject, GameObject foundsecondItemObject, GameObject selectedInventorySlot)
    {
        if (foundfirstItemObject != null && foundsecondItemObject != null)
        {
            Eventbus.Instance.Publish<TryToMergeItems_Signal>(new TryToMergeItems_Signal(foundfirstItemObject, foundsecondItemObject));
        }
        else if (foundfirstItemObject != null && selectedInventorySlot != null)
        {
            Eventbus.Instance.Publish<MouseButtonReleasedOnInventorySlot_Signal>(new MouseButtonReleasedOnInventorySlot_Signal(PlayersInventory.Instance, foundfirstItemObject, selectedInventorySlot));
        }
        else if (foundfirstItemObject != null)
        {
            Eventbus.Instance.Publish<MouseButtonReleasedOnChooseField_Signal>(new MouseButtonReleasedOnChooseField_Signal(foundfirstItemObject));
        }
    }
    private void ThereIsNoItemUnderMouse()
    {
        if (MouseInputManager.Instance.MouseInputState != MouseInputManager.StatesOfMouseInput.DraggingItem && MouseInputManager.Instance.CurrentItem == null)
        {
            Eventbus.Instance.Publish<MouseExitItem_Signal>(new MouseExitItem_Signal());
        }
    }
    private bool CheckObjectIsItem(RaycastResult result)
    {
        return result.gameObject.CompareTag("Item");
    }
    private bool CheckObjectIsInventorySlot(RaycastResult result)
    {
        return result.gameObject.CompareTag("InventorySlot");
    }

    private void CheckResultsListOnHitsAndMouseInFightingState()
    {
        GameObject foundfirstItemObject = null;
        foreach (RaycastResult result in results)
        {
            if (CheckObjectIsItem(result) && foundfirstItemObject == null)
            {
                foundfirstItemObject = result.gameObject;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (foundfirstItemObject != null && foundfirstItemObject.GetComponent<ItemsStaticInformationHolder>().Item.FormOfActivation == ItemStatic.FormsOfActivation.Manually)
            {
                Eventbus.Instance.Publish<PlayerWantsToUseItem_Signal>(new PlayerWantsToUseItem_Signal(foundfirstItemObject));
            }
        }
    }
}
