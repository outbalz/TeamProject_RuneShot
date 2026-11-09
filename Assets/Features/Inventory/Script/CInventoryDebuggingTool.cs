using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventoryDebuggingTool : MonoBehaviour
{
    [SerializeField] private CItemDataSO _itemDataSO;
    [SerializeField] private int _amount;

    [Space]
    [SerializeField] private CItemDataSO[] _startItems;

    [ContextMenu("Add Item")]
    private void AddItem()
    {
        if (_itemDataSO == null)
        {
            Debug.LogWarning("ItemDataSO is not assigned.");
            return;
        }
        CInventoryManager.Instance.AddToInventory(_itemDataSO, _amount);
    }

    private void Start()
    {
        for (int i = 0; i < _startItems.Length; i++)
        {
            CInventoryManager.Instance.AddToInventory(_startItems[i], _amount);
        }
    }
}
