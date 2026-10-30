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
	[SerializeField] private int _inventorySize = 20;
	#endregion

	#region 내부변수
	private CItemSlotData[] _inventory;
	private CItemSlotController[] _inventorySlots;
	#endregion

	#region 프로퍼티
    #endregion

    protected override void Awake()
    {
		base.Awake();

		_inventory = new CItemSlotData[_inventorySize];
		_inventorySlots = new CItemSlotController[_inventorySize];

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
			CItemSlotController slot = Instantiate(_itemSlotPrefab, _itemSlotLayout);

			if (slot == null)
			{
				continue;
			}

			_inventorySlots[i] = slot;
			slot.InitializeSlot(i);
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

		for (int i = 0; i < inst._inventory.Length; i++)
		{
			if (inst._inventory[i] == null || inst._inventory[i].Item == item)
			{
				AddToInventory(item, i, amount, out amount);

				if(amount <= 0)
				{
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
			return;
		}

        inst._inventorySlots[index].UpdateSlot(inst._inventory[index].Item);
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
