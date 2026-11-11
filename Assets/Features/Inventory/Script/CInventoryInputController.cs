using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventoryInputController : MonoBehaviour
{
    #region 인스펙터    
    [SerializeField] private GameObject _inventoyPanel;

    [Space]
    [Header("인풋 엑션")]
    [SerializeField] private string _scrollActionName = "Scroll";
    [SerializeField] private string _toggleInventoryActionName = "ToggleInventory";
    [SerializeField] private string _quickSoltAction1Name = "UseQuickSlot1";
    [SerializeField] private string _quickSoltAction2Name = "UseQuickSlot2";
    [SerializeField] private string _quickSoltAction3Name = "UseQuickSlot3";
    [SerializeField] private string _quickSoltAction4Name = "UseQuickSlot4";
    [SerializeField] private string _quickSoltAction5Name = "UseQuickSlot5";
    #endregion

    #region 내부변수
    private CInputAxisActionData _scrollAction;
    private CInputActionData _toggleInventoryAction;
    private CInputActionData _quickSoltAction1;
    private CInputActionData _quickSoltAction2;
    private CInputActionData _quickSoltAction3;
    private CInputActionData _quickSoltAction4;
    private CInputActionData _quickSoltAction5;

    private int _scrollIndex = 1;
    private bool _isReady = false;
    #endregion

    private void Start()
    {
        CInputDispatcher inputDispatcher = CInputDispatcher.Instance;

        if
        (
            !inputDispatcher.InputAxisActionMap.TryGetValue(_scrollActionName, out _scrollAction) ||
            !inputDispatcher.InputActionMap.TryGetValue(_toggleInventoryActionName, out _toggleInventoryAction) ||
            !inputDispatcher.InputActionMap.TryGetValue(_quickSoltAction1Name, out _quickSoltAction1) ||
            !inputDispatcher.InputActionMap.TryGetValue(_quickSoltAction2Name, out _quickSoltAction2) ||
            !inputDispatcher.InputActionMap.TryGetValue(_quickSoltAction3Name, out _quickSoltAction3) ||
            !inputDispatcher.InputActionMap.TryGetValue(_quickSoltAction4Name, out _quickSoltAction4) ||
            !inputDispatcher.InputActionMap.TryGetValue(_quickSoltAction5Name, out _quickSoltAction5)
        )
        {
            Debug.LogWarning("인풋엑션 누락");
            enabled = false;
            return;
        }

        TryBind();
    }

    private void OnEnable()
    {
        TryBind();
    }

    private void OnDisable()
    {
        UnBind();
    }

    private void OnDestroy()
    {
        UnBind();
    }

    private void TryBind()
    {
        if (_isReady)
        {
            return;
        }

        if
        (
            _scrollAction == null ||
            _toggleInventoryAction == null ||
            _quickSoltAction1 == null ||
            _quickSoltAction2 == null ||
            _quickSoltAction3 == null ||
            _quickSoltAction4 == null ||
            _quickSoltAction5 == null 
        )
        {
            return;
        }

        _scrollAction.OnPerformedAction += OnScroll;
        _toggleInventoryAction.OnPerformedAction += ToggleInventory;
        _quickSoltAction1.OnPerformedAction += UseQuickSlot1;
        _quickSoltAction2.OnPerformedAction += UseQuickSlot2;
        _quickSoltAction3.OnPerformedAction += UseQuickSlot3;
        _quickSoltAction4.OnPerformedAction += UseQuickSlot4;
        _quickSoltAction5.OnPerformedAction += UseQuickSlot5;

        _isReady = true;
    }

    private void UnBind()
    {
        if (!_isReady)
        {
            return;
        }

        if
        (
            _scrollAction == null ||
            _toggleInventoryAction == null ||
            _quickSoltAction1 == null ||
            _quickSoltAction2 == null ||
            _quickSoltAction3 == null ||
            _quickSoltAction4 == null ||
            _quickSoltAction5 == null
        )
        {
            return;
        }

        _scrollAction.OnPerformedAction -= OnScroll;
        _toggleInventoryAction.OnPerformedAction-= ToggleInventory;
        _quickSoltAction1.OnPerformedAction -= UseQuickSlot1;
        _quickSoltAction2.OnPerformedAction -= UseQuickSlot2;
        _quickSoltAction3.OnPerformedAction -= UseQuickSlot3;
        _quickSoltAction4.OnPerformedAction -= UseQuickSlot4;
        _quickSoltAction5.OnPerformedAction -= UseQuickSlot5;

        _isReady = false;
    }

    private void UseQuickSlot1()
    {
        UseQuickSlot(1);
    }

    private void UseQuickSlot2()
    {
        UseQuickSlot(2);
    }

    private void UseQuickSlot3()
    {
        UseQuickSlot(3);
    }

    private void UseQuickSlot4()
    {
        UseQuickSlot(4);
    }

    private void UseQuickSlot5()
    {
        UseQuickSlot(5);
    }

    private void UseQuickSlot(int slotIndex)
    {
        CItemSlotData slot = CInventoryManager.Instance[slotIndex];

        if (slot != null && slot.Item != null)
        {
            if (slot.Item.OnUse())
            {
                if (slot.Item.ItemType == EItemtype.Consumable)
                {
                    CInventoryManager.Instance.RemoveFromInventory(slotIndex, 1);
                }
            }
        }

        //Debug.Log(slotIndex);
    }

    private void OnScroll(float scroll)
    {
        if(scroll == 0)
        {
            return;
        }

        if (scroll > 0)
        {
            _scrollIndex++;

            if (_scrollIndex > 2)
            {
                _scrollIndex = 1;
            }
        }

        else if (scroll < 0)
        {
            _scrollIndex--;

            if(_scrollIndex < 1)
            {
                _scrollIndex = 2;
            }
        }

        UseQuickSlot(_scrollIndex);
    }

    private void ToggleInventory()
    {
        _inventoyPanel.SetActive(!_inventoyPanel.activeSelf);
    }
}
