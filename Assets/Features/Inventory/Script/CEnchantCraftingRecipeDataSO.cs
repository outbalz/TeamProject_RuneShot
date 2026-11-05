using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnchantCraftingRecipeDataSO : CGameDataSO
{
    [System.Serializable]
    public class CIngredient
    {
        [SerializeField] private CItemDataSO _ingredientItem;
        [SerializeField] private int _amount;

        public CItemDataSO Item => _ingredientItem;
        public int Amount => _amount;
    }

    #region 인스팩터
    [SerializeField] private CIngredient[] _ingredient;
    [SerializeField] private CItemDataSO _result;
    [SerializeField] private int _resultAmount;
    #endregion

    #region 프로퍼티
    public CIngredient[] Ingredients => _ingredient;
    public CItemDataSO Result => _result;
    public int Amount => _resultAmount;
    #endregion

}
