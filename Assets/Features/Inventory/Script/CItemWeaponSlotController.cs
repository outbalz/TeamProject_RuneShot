using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CItemWeaponSlotController : CItemSlotController
{

    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropedGO = eventData.pointerDrag;
        CDragableItemController item = dropedGO.GetComponent<CDragableItemController>();

        if (item == null)
        {
            return;
        }

        if (item.Itemtype != EItemtype.Weapon) 
        {
            return;
        }

        item.SetSlot(this);
    }
}
