using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

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

public interface IitemContainer
{

	public IReadOnlyList<CItemSlotData> Inventory { get; }
	public CItemSlotData this[int index] { get; }
	public bool AddToInventory(CItemDataSO item, int amount);
    public bool AddToInventory(CItemDataSO item, int index, int amount, out int leftAmount);
	public void RemoveFromInventory(int index);
	public void RemoveFromInventory(int index, int amount);
}

public class CInventoryManager : CSingleton<CInventoryManager> , IitemContainer
{
	#region 인스팩터
	[SerializeField] private Transform _itemSlotLayout;
	[SerializeField] private CItemSlotController _itemSlotPrefab;
	[SerializeField] private int _inventorySize = 25;
	[SerializeField] private List<CItemSlotController> _inventorySlots;
	#endregion

	#region 내부변수
	private List<CItemSlotData> _inventory = new List<CItemSlotData>();
	private Dictionary<CItemDataSO, List<int>> _inventoryItemMap = new Dictionary<CItemDataSO, List<int>>();
	#endregion

	#region 프로퍼티
	public IReadOnlyList<CItemSlotData> Inventory => _inventory;
	public IReadOnlyDictionary<CItemDataSO, List<int>> InventoryItemMap => _inventoryItemMap;
	#endregion

	#region 인덱서
	public CItemSlotData this[int index] => _inventory[index];
	#endregion

	protected override void Awake()
    {
		base.Awake();

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
            _inventorySlots[i].InitializeSlot(i, this, null);
        }
    }

    public bool AddToInventory(CItemDataSO item, int amount)
    {

		if (_inventoryItemMap.ContainsKey(item))
		{
			for (int i = 0; i < _inventoryItemMap[item].Count; i++)
			{
				AddToInventory(item, _inventoryItemMap[item][i], amount, out amount);
			}

			if(amount <= 0)
			{
				CEnchantCraftingManager.Instance.UpdatePanelIfActive();
				return true;
			}
		}

		for (int i = 0; i <_inventory.Count; i++)
		{
			if (_inventorySlots[i].SlotType != EItemtype.None)
			{
				continue;
            }

			if (_inventory[i] == null || _inventory[i].Item == item)
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

    public bool AddToInventory(CItemDataSO item, int index, int amount, out int leftAmount)
	{

		if(_inventory[index] == null)
		{

			_inventory[index] = new CItemSlotData(item, amount);
			_inventorySlots[index].UpdateSlot(item);

			if (amount <= item.MaxStack)
			{
				leftAmount = 0;
			}

			else
			{
				leftAmount = amount - item.MaxStack;
			}

			if (_inventoryItemMap.ContainsKey(item))
			{
				_inventoryItemMap[item].Add(index);
			}

			else
			{
                _inventoryItemMap.Add(item, new List<int> {index});
            }

			CEnchantCraftingManager.Instance.UpdatePanelIfActive();
			return true;
		}

		if(_inventory[index].Item != item && _inventory[index].Item != null)
		{
			Debug.Log("이미 이 아이템 슬롯을 점유하고 있음");
			leftAmount = amount;
			return false;
		}

		if(_inventory[index].AddToSlot(item, amount, out leftAmount))
		{
			_inventorySlots[index].UpdateSlot(item);

            CEnchantCraftingManager.Instance.UpdatePanelIfActive();
            return true;
		}

		leftAmount = amount;
		return false;
    }

	public void RemoveFromInventory(int index)
	{


        if (_inventory[index] == null)
        {
            Debug.LogWarning("이 아이템슬롯은 이미 비어있음");
            return;
        }

		if (_inventoryItemMap.ContainsKey(_inventory[index].Item))
		{
			if (_inventoryItemMap[_inventory[index].Item].Count == 1)
			{
				_inventoryItemMap[_inventory[index].Item] = null;
				_inventoryItemMap.Remove(_inventory[index].Item);
			}

			else
			{
				_inventoryItemMap[_inventory[index].Item].Remove(index);
			}
		}

        _inventory[index] = null;
        _inventorySlots[index].UpdateSlot(null);
        CEnchantCraftingManager.Instance.UpdatePanelIfActive();
    }

	public void RemoveFromInventory(int index, int amount)
	{

        if (_inventory[index] == null)
        {
            Debug.LogWarning("이 아이템슬롯은 이미 비어있음");
            return;
        }

		if (!_inventory[index].RemoveFormSlot(amount))
        {
            if (_inventoryItemMap.ContainsKey(_inventory[index].Item))
            {
                if (_inventoryItemMap[_inventory[index].Item].Count == 1)
                {
                    _inventoryItemMap[_inventory[index].Item] = null;
                    _inventoryItemMap.Remove(_inventory[index].Item);
                }

                else
                {
                    _inventoryItemMap[_inventory[index].Item].Remove(index);
                }
            }

            _inventory[index] = null;
			_inventorySlots[index].UpdateSlot(null);
            CEnchantCraftingManager.Instance.UpdatePanelIfActive();
            return;
		}

        _inventorySlots[index].UpdateSlot(_inventory[index].Item);
        CEnchantCraftingManager.Instance.UpdatePanelIfActive();
    }

	[ContextMenu("SortInventory")]
	public void SortInventory()
	{
		List<CItemSlotData> inventoryWithItem = new List<CItemSlotData>();

		for (int i = 0; i < _inventory.Count; i++)
		{
			if (_inventory[i] != null)
			{
				inventoryWithItem.Add(_inventory[i]);
			}
		}

		CItemSlotData[] sortArr = inventoryWithItem.OrderBy(x => x.Item.Name).ToArray();

		for (int i = 0; i < _inventory.Count; i++)
		{
			if (_inventory[i] != null)
			{
				RemoveFromInventory(i);
			}
        }

		for (int i = 0; i < sortArr.Length; i++)
		{
			AddToInventory(sortArr[i].Item, sortArr[i].Amount);
		}
	}
}
