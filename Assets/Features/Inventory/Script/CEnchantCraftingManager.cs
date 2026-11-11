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
        for (int i = _panels.Count -1; i >= 0; i--)
        {
            if (IngredientCheck(_panels[i].RecipeData))
            {
                _panels[i].SetPanelActive(true);
                _panels[i].transform.SetAsFirstSibling();
            }

            else
            {
                _panels[i].SetPanelActive(false);
                _panels[i].transform.SetAsLastSibling();
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
                if(invnetory[j] == null)
                {
                    continue;
                }

                if (invnetory[j].Item == ingredientItem)
                {
                    int slotAmount = Mathf.Min(invnetory[j].Amount, amount);
                    amount -= slotAmount;

                    CInventoryManager.Instance.RemoveFromInventory(j, slotAmount);

                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }

        }

        CInventoryManager.Instance.AddToInventory(recipe.Result, recipe.Amount);
    }

    public void UpdatePanelIfActive()
    {
        if (gameObject.activeSelf)
        {
            UpdatePanel();
        }
    }

    public void ToggleUI()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        UpdatePanelIfActive();
    }
}
