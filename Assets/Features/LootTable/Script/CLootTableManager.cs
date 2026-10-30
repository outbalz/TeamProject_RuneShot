using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewardable
{

}

[System.Serializable]
public class CLootTable
{
    public string LootTableName;
    public List<CLootTableElement> LootTableElements;
}

[System.Serializable]
public class CLootTableElement
{
    #region 인스팩터
    [SerializeField] private float _weight;
    [SerializeField] private List<string> _modifiers;
    [SerializeField] private IRewardable _element;
    [SerializeField] private float _luckModifier;
    #endregion

    #region 프로퍼티
    public IRewardable Element => _element;
    public float LuckModifier => _luckModifier;
    public float Weight
    {
        get 
        { 
            float weight = _weight;

            if(_modifiers == null || _modifiers.Count == 0)
            {
                return weight;
            }

            float modifier = 0;

            for (int i = 0; i < _modifiers.Count; i++)
            {
                modifier = CModifierHandler.Instance.GetModifiedValue(_modifiers[i], modifier);
            }

            modifier = modifier * _luckModifier;

            return weight + modifier; 
        }
    }
    #endregion
}

public class CLootTableManager : CSingleton<CLootTableManager>
{
    #region 인스팩터
    [SerializeField] private List<CLootTable> _lootTables;
    #endregion

    #region 내부변수
    private Dictionary<string, CLootTable> _lootTableMap = new Dictionary<string, CLootTable>();
    #endregion

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < _lootTables.Count; i++)
        {
            if (string.IsNullOrWhiteSpace(_lootTables[i].LootTableName))
            {
                continue;
            }

            _lootTableMap.Add(_lootTables[i].LootTableName, _lootTables[i]);
        }
    }

    public static CLootTable GetLootTable(string lootTableName)
    {
        if (!Instance._lootTableMap.ContainsKey(lootTableName))
        {
            return null;
        }

        return Instance._lootTableMap[lootTableName];
    }

    public static IRewardable[] GetLootTableElements(string lootTableName, int amount = 1)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("잘못된 메게변수 입력: 루트태이블에서 1개이상의 요소를 호출해야합니다.");
            return null;
        }

        IRewardable[] rewards = new IRewardable[amount];

        for (int i = 0; i < amount; i++)
        {
            rewards[i] = Instance.GetLootTableElement(lootTableName);
        }

        return rewards;
    }

    private IRewardable GetLootTableElement(string lootTableName)
    {
        CLootTable lootTable = _lootTableMap[lootTableName];
        
        float ranMax = 0;

        for (int i = 0; i < lootTable.LootTableElements.Count; i++)
        {
            ranMax += lootTable.LootTableElements[i].Weight;
        }

        float ranNum = Random.Range(0f, ranMax);

        float resultNum = 0f;

        IRewardable element = null;

        for (int i = 0; i < lootTable.LootTableElements.Count; i++)
        {
            resultNum += lootTable.LootTableElements[i].Weight;

            if (resultNum >= ranNum)
            {
                element = lootTable.LootTableElements[i].Element;
                break;
            }
        }

        return element;
    }
}
