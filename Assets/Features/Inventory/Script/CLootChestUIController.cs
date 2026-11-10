using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CLootChestUIController : CSingleton<CLootChestUIController>
{

	#region 인스팩터
	[SerializeField] private CItemSlotController[] _slots;
	[SerializeField] private GameObject _panel;
	[SerializeField] private TMP_Text _chestNameText;
	#endregion

	#region 프로퍼티
	public CItemSlotController[] Slots => _slots;
    #endregion

    protected override void Awake()
    {
		base.Awake();
        _panel.SetActive(false);
    }

    public void UpdateSlot(CItemContainerInventoryHandler container)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			if (i >= container.InventorySize)
			{
				_slots[i].gameObject.SetActive(false);
				continue;
			}

			_slots[i].gameObject.SetActive(true);

			CItemDataSO item = null;

			if(container.Inventory[i] != null)
			{
				item = container.Inventory[i].Item;
			}
			_slots[i].InitializeSlot(i, container, item);
		}
	}

	public void ToggleUI(bool toggle)
	{
		_panel.SetActive(toggle);
	}
}
