using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/LootChestSO", fileName = "LootChestSO_")]
public class CLootChestDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] private Sprite _chestSprite;
    [SerializeField] private int _invnentorySize = 5;
    [SerializeField] private string _lootTableID = "LootTable_ChestLootTable";
    #endregion

    #region 프로퍼티
    public Sprite ChestSprite => _chestSprite;
    public int InventorySize => _invnentorySize;
    public string LootTableID => _lootTableID;
    #endregion

}
