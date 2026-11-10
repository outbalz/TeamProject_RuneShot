using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDataSO/LootTableSO", fileName = "LootTableSO_")]
public class CLootTableDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] private List<CLootTableElement> _lootTableElements;
    #endregion

    #region 프로퍼티
    public IReadOnlyList<CLootTableElement> LootTableElements => _lootTableElements;
    #endregion
}
