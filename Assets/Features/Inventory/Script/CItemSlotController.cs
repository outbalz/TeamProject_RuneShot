using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CItemSlotController : MonoBehaviour, IDropHandler
{
	#region 인스팩터
	[SerializeField] private CDragableItemController _item;
    [SerializeField] private EItemtype _slotType = EItemtype.None;
    #endregion

    #region 내부변수
    private int _slotIndex;
    private IitemContainer _itemContainer;
    #endregion

    #region 프로퍼티
    public int SlotIndex => _slotIndex;
    public EItemtype SlotType => _slotType;
    public IitemContainer ItemContainer => _itemContainer;
    #endregion

    public void InitializeSlot(int index, IitemContainer container, CItemDataSO item = null)
    {
        _slotIndex = index;
        _itemContainer = container;
        UpdateSlot(item);
    }

    public void UpdateSlot(CItemDataSO item)
    {
        _item.UpdateSlotItem(item);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        GameObject dropedGO = eventData.pointerDrag;
        CDragableItemController item =  dropedGO.GetComponent<CDragableItemController>();

        if (item == null)
        {
            return;
        }

        item.SetSlot(this);

    }

}
