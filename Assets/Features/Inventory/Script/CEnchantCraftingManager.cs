using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnchantCraftingManager : MonoBehaviour
{
    private bool IngredientCheck(CEnchantCraftingRecipeDataSO recipe)
    {
        IReadOnlyList<CItemSlotData> invnetory = CInventoryManager.Instance.Inventory;

        for (int i = 0; i < recipe.Ingredients.Length; i++)
        {
            CItemDataSO ingredientItem = recipe.Ingredients[i].Item;
            int amount = recipe.Ingredients[i].Amount;


            for (int j = 0; j < invnetory.Count; j++)
            {
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
