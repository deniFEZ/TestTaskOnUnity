using UnityEngine;
using UnityEngine.Rendering;

public class MouseInputManager : MonoBehaviour
{
    public static MouseInputManager Instance { get; private set; }
    public enum StatesOfMouseInput { Nothing, OnItem, DraggingItem }
    public StatesOfMouseInput MouseInputState { get; private set; }
    public GameObject CurrentItem { get; private set; }

    private void MouseOnItem(MouseEnterItem_Signal signal)
    {
        ChangeCurrentMouseInputState(StatesOfMouseInput.OnItem);
        CurrentItem = signal.Item;
    }
    private void MouseExitItem(MouseExitItem_Signal signal)
    {
        ChangeCurrentMouseInputState(StatesOfMouseInput.Nothing);
        CurrentItem = null;
    }
    private void MouseDraggingItemFromInventory(MouseStartToDragItemFromInventory_Signal signal)
    {
        ChangeCurrentMouseInputState(StatesOfMouseInput.DraggingItem);
        CurrentItem = signal.itemObject;
    }
    private void MouseDraggingItem(MouseStartToDragItem_Signal signal)
    {
        ChangeCurrentMouseInputState(StatesOfMouseInput.DraggingItem);
        CurrentItem = signal.itemObject;
    }
    private void MouseButtonReleasedOnInventorySlot(MouseButtonReleasedOnInventorySlot_Signal signal)
    {
        ChangeCurrentMouseInputState(StatesOfMouseInput.Nothing);
        CurrentItem = null;
    }
    private void MouseTryToMergeItems(TryToMergeItems_Signal signal)
    {
        ChangeCurrentMouseInputState(StatesOfMouseInput.Nothing);
        CurrentItem = null;
    }
    private void MouseButtonReleasedOnChooseField(MouseButtonReleasedOnChooseField_Signal signal)
    {
        ChangeCurrentMouseInputState(StatesOfMouseInput.Nothing);
        CurrentItem = null;
    }
    private void MouseButtonDown_SignalCatched(MouseButtonDown_Signal signal)
    {
        //MouseButtonDown Dont work as needed
    }

    private void Awake()
    {
        LockCursor();//TEMPORARY FUNCTION
        CheckSignleton();
        ChangeCurrentMouseInputState(StatesOfMouseInput.Nothing);
        SubscribeToEventbusEvents();
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
        UnsubscribeFromEventbusEvents();
    }
    private void DestroyInstance()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void LockCursor() //TEMPORARY FUNCTION
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1;
    } 
    private void SubscribeToEventbusEvents()
    {
        if (IfEventbusCreated())
        {
            Eventbus.Instance.Subscribe<MouseEnterItem_Signal>(MouseOnItem);
            Eventbus.Instance.Subscribe<MouseExitItem_Signal>(MouseExitItem);
            Eventbus.Instance.Subscribe<MouseStartToDragItemFromInventory_Signal>(MouseDraggingItemFromInventory);
            Eventbus.Instance.Subscribe<MouseButtonReleasedOnInventorySlot_Signal>(MouseButtonReleasedOnInventorySlot);
            Eventbus.Instance.Subscribe<MouseStartToDragItem_Signal>(MouseDraggingItem);
            Eventbus.Instance.Subscribe<TryToMergeItems_Signal>(MouseTryToMergeItems);
            Eventbus.Instance.Subscribe<MouseButtonReleasedOnChooseField_Signal>(MouseButtonReleasedOnChooseField);
            Eventbus.Instance.Subscribe<MouseButtonDown_Signal>(MouseButtonDown_SignalCatched);
        }
    }
    private void UnsubscribeFromEventbusEvents()
    {
        if (IfEventbusCreated())
        {
            Eventbus.Instance.Unsubscribe<MouseEnterItem_Signal>(MouseOnItem);
            Eventbus.Instance.Unsubscribe<MouseExitItem_Signal>(MouseExitItem);
            Eventbus.Instance.Unsubscribe<MouseStartToDragItemFromInventory_Signal>(MouseDraggingItemFromInventory);
            Eventbus.Instance.Unsubscribe<MouseButtonReleasedOnInventorySlot_Signal>(MouseButtonReleasedOnInventorySlot);
            Eventbus.Instance.Unsubscribe<MouseStartToDragItem_Signal>(MouseDraggingItem);
            Eventbus.Instance.Unsubscribe<TryToMergeItems_Signal>(MouseTryToMergeItems);
            Eventbus.Instance.Unsubscribe<MouseButtonReleasedOnChooseField_Signal>(MouseButtonReleasedOnChooseField);
            Eventbus.Instance.Unsubscribe<MouseButtonDown_Signal>(MouseButtonDown_SignalCatched);
        }
    }
    private bool IfEventbusCreated()
    {
        return Eventbus.Instance != null;
    }
    private void ChangeCurrentMouseInputState(StatesOfMouseInput newState)
    {
        MouseInputState = newState;
    }
}
