using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CDragableItemController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region inspector
    [SerializeField] private Image _itemImage;
    [SerializeField] private CItemSlotController _slot;
    [SerializeField] private Text _amountText;
    #endregion

    #region private var
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

        if(CInventoryManager.AddToInventory(_itemData, slot.SlotIndex, CInventoryManager.GetItemAmount(_slot.SlotIndex)))
        {
            CInventoryManager.RemoveFromInventory(_slot.SlotIndex);
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
