using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CDragableItemController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region 인스팩터
    [SerializeField] private Image _itemImage;
    [SerializeField] private CItemSlotController _slot;
    [SerializeField] private Text _amountText;
    #endregion

    #region 내부변수
    private CItemDataSO _itemData;
    #endregion

    #region 프로퍼티
    public CItemDataSO ItemData => _itemData;
    public EItemtype Itemtype => _itemData.ItemType;
    #endregion

    private void Awake()
    {
        if(_itemImage ==  null || _amountText == null)
        {
            Debug.LogWarning("Missing UI element");
            enabled = false;
            return;
        }
    }

    public void UpdateSlotItem(CItemDataSO item)
    {
        _itemData = item;

        if (_itemData != null)
        {
            _itemImage.enabled = true;
            _amountText.enabled = true;
            _itemImage.sprite = _itemData.ItemSprite;
            _amountText.text = $"{_slot.ItemContainer[_slot.SlotIndex].Amount}";
        }

        else
        {
            _itemImage.enabled = false;
            _amountText.enabled = false;
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _itemImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_slot.transform);
        _itemImage.raycastTarget = true;
    }

    public void SetSlot(CItemSlotController slot)
    {
        if (_slot == slot)
        {
            return;
        }

        if(slot.ItemContainer == null || _slot.ItemContainer == null)
        {
            Debug.LogWarning("아이템 컨테이너 누락");
            return;
        }

        IitemContainer originContainer = _slot.ItemContainer;
        IitemContainer targetContainer = slot.ItemContainer;

        int amount = originContainer[_slot.SlotIndex].Amount;
        int leftAmount;

        CItemSlotData originSlotData = originContainer[_slot.SlotIndex];
        CItemSlotData targetSlotData = targetContainer[slot.SlotIndex];

        if (targetSlotData != null)
        {
            if (targetSlotData.Item != _itemData)
            {
                originContainer.RemoveFromInventory(_slot.SlotIndex);
                targetContainer.RemoveFromInventory(slot.SlotIndex);

                originContainer.AddToInventory(targetSlotData.Item, _slot.SlotIndex, targetSlotData.Amount, out leftAmount);
                targetContainer.AddToInventory(originSlotData.Item, slot.SlotIndex, originSlotData.Amount, out leftAmount);
                return;
            }
        }

        if (targetContainer.AddToInventory(_itemData, slot.SlotIndex, amount, out leftAmount))
        {
            originContainer.RemoveFromInventory(_slot.SlotIndex, amount - leftAmount);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_itemData != null)
        {
            CTooltipController.ShowTooltip(_itemData.Name, _itemData.Description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CTooltipController.HideTooltip();
    }
}
