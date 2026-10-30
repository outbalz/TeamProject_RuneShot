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
            _amountText.text = $"{CInventoryManager.GetItemAmount(_slot.SlotIndex)}";
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

        int amount = CInventoryManager.GetItemAmount(_slot.SlotIndex);
        int leftAmount;

        CItemSlotData originSlotData = CInventoryManager.GetSlotData(_slot.SlotIndex);
        CItemSlotData targetSlotData = CInventoryManager.GetSlotData(slot.SlotIndex);

        if (targetSlotData != null)
        {
            if (targetSlotData.Item != _itemData)
            {
                CInventoryManager.RemoveFromInventory(_slot.SlotIndex);
                CInventoryManager.RemoveFromInventory(slot.SlotIndex);

                CInventoryManager.AddToInventory(originSlotData.Item, slot.SlotIndex, originSlotData.Amount, out leftAmount);
                CInventoryManager.AddToInventory(targetSlotData.Item, _slot.SlotIndex, targetSlotData.Amount, out leftAmount);
                return;
            }
        }

        if (CInventoryManager.AddToInventory(_itemData, slot.SlotIndex, amount, out leftAmount))
        {
            CInventoryManager.RemoveFromInventory(_slot.SlotIndex, amount - leftAmount);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO Tolltip On
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO Tolltip Off
    }
}
