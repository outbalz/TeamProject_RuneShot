using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CItemSlotData
{
	#region 인스팩터
	[SerializeField] private CItemDataSO _itemData;
	[SerializeField] private int _amount;
	#endregion

	#region 내부변수
	public CItemDataSO Item => _itemData;
	public int Amount => _amount;
	#endregion

	public bool AddToSlot(CItemDataSO item, int amount, out int leftAmount)
	{
		if (item != _itemData)
		{
			leftAmount = amount;
			return false;
		}

		_amount += amount;

		if (_amount > item.MaxStack)
		{
			leftAmount = _amount - item.MaxStack;
			_amount = item.MaxStack;
		}

		else
		{
			leftAmount = 0;
		}

		return true;
	}

	public void RemoveAllFormSlot()
	{
		_amount = 0;
	}

	public bool RemoveFormSlot(int amount)
	{
		if(_amount - amount <= 0)
		{
			return false;
		}

		_amount -= amount;
		return true;
	}

	public CItemSlotData(CItemDataSO item, int amount)
	{
		_itemData = item;
		_amount = amount;

		if(amount > item.MaxStack)
		{
            _amount = item.MaxStack;
		}
	}
}

public class CInventoryManager : CSingleton<CInventoryManager> 
{
	#region 인스팩터
	[SerializeField] private Transform _itemSlotLayout;
	[SerializeField] private CItemSlotController _itemSlotPrefab;
	[SerializeField] private int _inventorySize = 25;
	[SerializeField] private List<CItemSlotController> _inventorySlots;
	#endregion

	#region 내부변수
	private List<CItemSlotData> _inventory = new List<CItemSlotData>();
	#endregion

	#region 프로퍼티
	public IReadOnlyList<CItemSlotData> Inventory => _inventory;
	#endregion

	#region 인덱서
	public CItemSlotData this[int index] => _inventory[index];
	#endregion

	protected override void Awake()
    {
		base.Awake();

		//_inventory = new CItemSlotData[_inventorySize];
		//_inventorySlots = new CItemSlotController[_inventorySize];

		if(_itemSlotLayout == null || _itemSlotPrefab == null)
		{
			Debug.LogWarning("아이템 슬롯 요소 누락");
		}

		InitializeInventory();
    }

    private void InitializeInventory()
    {
		if(_itemSlotPrefab == null)
		{
			Debug.LogWarning("아이템 슬롯 프리팹 누락");
			return;
		}
		

		for (int i = 0; i < _inventorySize; i++)
		{
			if (_inventorySlots.Count -1 < i)
			{

				CItemSlotController slot = Instantiate(_itemSlotPrefab, _itemSlotLayout);
				
				if (slot == null)
				{
					continue;
				}

				_inventorySlots.Add(slot);
			}

			_inventory.Add(null);
            //_inventorySlots[i] = slot;
            _inventorySlots[i].InitializeSlot(i);
        }
    }

    public static bool AddToInventory(CItemDataSO item, int amount)
    {
        if (Instance == null)
        {
            Debug.LogWarning("아이탬 메니저 인스턴스 누락");
            return false;
        }

        CInventoryManager inst = Instance;

		for (int i = 0; i < inst._inventory.Count; i++)
		{
			if (inst._inventorySlots[i].SlotType != EItemtype.None)
			{
				continue;
            }

			if (inst._inventory[i] == null || inst._inventory[i].Item == item)
			{
				AddToInventory(item, i, amount, out amount);

				if(amount <= 0)
				{
					CEnchantCraftingManager.Instance.UpdatePanelIfActive();
					return true;
				}
			}
		}

		Debug.LogWarning("인밴토리가 가득참");
		return false;
    }

    public static bool AddToInventory(CItemDataSO item, int index, int amount, out int leftAmount)
	{
		if (Instance == null)
		{
			Debug.LogWarning("아이탬 메니저 인스턴스 누락");
			leftAmount = amount;
			return false;
		}

        CInventoryManager inst = Instance;

		if(inst._inventory[index] == null)
		{

			inst._inventory[index] = new CItemSlotData(item, amount);
			inst._inventorySlots[index].UpdateSlot(item);

			if (amount <= item.MaxStack)
			{
				leftAmount = 0;
			}

			else
			{
				leftAmount = amount - item.MaxStack;
			}

			CEnchantCraftingManager.Instance.UpdatePanelIfActive();
			return true;
		}

		if(inst._inventory[index].Item != item && inst._inventory[index].Item != null)
		{
			Debug.Log("이미 이 아이템 슬롯을 점유하고 있음");
			leftAmount = amount;
			return false;
		}

		if(inst._inventory[index].AddToSlot(item, amount, out leftAmount))
		{
			inst._inventorySlots[index].UpdateSlot(item);

            CEnchantCraftingManager.Instance.UpdatePanelIfActive();
            return true;
		}

		leftAmount = amount;
		return false;
    }

	public static void RemoveFromInventory(int index)
	{
		if (Instance == null)
		{
			Debug.LogWarning("아이탬 메니저 인스턴스 누락");
			return;
		}

		CInventoryManager inst = Instance;

        if (inst._inventory[index] == null)
        {
            Debug.LogWarning("이 아이템슬롯은 이미 비어있음");
            return;
        }

        inst._inventory[index] = null;
        inst._inventorySlots[index].UpdateSlot(null);
        CEnchantCraftingManager.Instance.UpdatePanelIfActive();
    }

	public static void RemoveFromInventory(int index, int amount)
	{
		if (Instance == null)
		{
			Debug.LogWarning("아이탬 메니저 인스턴스 누락");
			return;
		}

		CInventoryManager inst = Instance;

        if (inst._inventory[index] == null)
        {
            Debug.LogWarning("이 아이템슬롯은 이미 비어있음");
            return;
        }

		if (!inst._inventory[index].RemoveFormSlot(amount))
		{
			inst._inventory[index] = null;
			inst._inventorySlots[index].UpdateSlot(null);
            CEnchantCraftingManager.Instance.UpdatePanelIfActive();
            return;
		}

        inst._inventorySlots[index].UpdateSlot(inst._inventory[index].Item);
        CEnchantCraftingManager.Instance.UpdatePanelIfActive();
    }

	public static CItemSlotData GetSlotData(int index)
	{
		if(Instance == null)
		{
			return null;
		}

		return Instance._inventory[index];
	}

	public static int GetItemAmount(int index)
	{
		if (Instance == null)
		{
			return 0;
		}

		return Instance._inventory[index].Amount;
	}
}
