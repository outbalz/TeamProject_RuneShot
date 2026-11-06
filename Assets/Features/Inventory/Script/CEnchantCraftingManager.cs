using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnchantCraftingManager : CSingleton<CEnchantCraftingManager>
{
    #region 인스팩터
    [SerializeField] private CEnchantCraftingPanelController _panelPrefab;
    [SerializeField] private Transform _panelLayout;
    #endregion

    #region 내부변수
    private List<CEnchantCraftingPanelController> _panels = new List<CEnchantCraftingPanelController>();
    #endregion

    private void Start()
    {
        InitializePanels();
        UpdatePanel();
    }

    private void InitializePanels()
    {
        CEnchantCraftingRecipeDatabaseSO recipeDatabase = CDataBaseManager.Instance.EnchantCraftingRecipeDB;

        for (int i = 0; i < recipeDatabase.Count; i++)
        {
            CEnchantCraftingPanelController panel = Instantiate(_panelPrefab, _panelLayout);

            panel.InitializePanel(recipeDatabase[i]);

            _panels.Add(panel);
        }
    }

    public void UpdatePanel()
    {
        for (int i = 0; i < _panels.Count; i++)
        {
            if (IngredientCheck(_panels[i].RecipeData))
            {
                //_panels[i].gameObject.SetActive(true);
            }

            else
            {
                //_panels[i].gameObject.SetActive(false);
            }
        }
    }


    private bool IngredientCheck(CEnchantCraftingRecipeDataSO recipe)
    {
        IReadOnlyList<CItemSlotData> invnetory = CInventoryManager.Instance.Inventory;

        for (int i = 0; i < recipe.Ingredients.Length; i++)
        {
            CItemDataSO ingredientItem = recipe.Ingredients[i].Item;
            int amount = recipe.Ingredients[i].Amount;


            for (int j = 0; j < invnetory.Count; j++)
            {
                if(invnetory[j] == null)
                {
                    continue;
                }

                if (invnetory[j].Item == ingredientItem)
                {
                    amount -= invnetory[j].Amount;

                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }

            if(amount > 0)
            {
                return false;
            }

        }

        return true;
    }

    public void CraftItem(CEnchantCraftingRecipeDataSO recipe)
    {
        if (!IngredientCheck(recipe))
        {
            return;
        }

        IReadOnlyList<CItemSlotData> invnetory = CInventoryManager.Instance.Inventory;

        for (int i = 0; i < recipe.Ingredients.Length; i++)
        {
            CItemDataSO ingredientItem = recipe.Ingredients[i].Item;
            int amount = recipe.Ingredients[i].Amount;


            for (int j = 0; j < invnetory.Count; j++)
            {
                if (invnetory[j].Item == ingredientItem)
                {
                    int slotAmount = Mathf.Max(invnetory[j].Amount, amount);
                    amount -= slotAmount;

                    CInventoryManager.RemoveFromInventory(j, slotAmount);

                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }

        }

        CInventoryManager.AddToInventory(recipe.Result, recipe.Amount);
    }


}
