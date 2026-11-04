using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventoryDebuggingTool : MonoBehaviour
{
    [SerializeField] private CItemDataSO _itemDataSO;
    [SerializeField] private int _amount;

    [ContextMenu("Add Item")]
    private void AddItem()
    {
        if (_itemDataSO == null)
        {
            Debug.LogWarning("ItemDataSO is not assigned.");
            return;
        }
        CInventoryManager.AddToInventory(_itemDataSO, _amount);
    }
}
