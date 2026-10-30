using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CItemSlotController : MonoBehaviour, IDropHandler
{
	#region 인스팩터
	[SerializeField] private CDragableItemController _item;
	#endregion

	#region 내부변수
	[SerializeField] private int _slotIndex;
    #endregion

    #region 프로퍼티
    public int SlotIndex => _slotIndex;
    #endregion

    public void InitializeSlot(int index, CItemDataSO item = null)
    {
        _slotIndex = index;
        UpdateSlot(item);
    }

    public void UpdateSlot(CItemDataSO item)
    {
        _item.UpdateSlotItem(item);
    }

    public void OnDrop(PointerEventData eventData)
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
