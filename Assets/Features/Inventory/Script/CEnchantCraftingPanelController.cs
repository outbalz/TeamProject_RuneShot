using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CEnchantCraftingPanelController : MonoBehaviour
{
	#region 인스팩터
	[SerializeField] private CEnchantCraftingRecipeDataSO _recipeData;

    [SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] private CItemIconController _resultItemIcon;
    [SerializeField] private TMP_Text _recipeNameText;
    [SerializeField] private CItemIconController _ingredientItemIconPrefab;
	[SerializeField] private List<CItemIconController> _ingredientItemIcons = new List<CItemIconController>();
    [SerializeField] private Transform _ingredientItemIconLayout;
    [SerializeField] private Button _craftButton;
    #endregion

    #region 프로퍼티
    public CEnchantCraftingRecipeDataSO RecipeData => _recipeData;
    #endregion

    private void Awake()
    {
        if(_canvasGroup == null|| _resultItemIcon ==  null || _recipeNameText == null || 
            _ingredientItemIconPrefab == null || _ingredientItemIconLayout == null || _craftButton == null)
        {
            Debug.LogWarning("인스펙터 요소 누락");
            gameObject.SetActive(false);
            enabled = false;
        }
    }

    public void InitializePanel(CEnchantCraftingRecipeDataSO recipe)
    {
        if(recipe != null)
        {
            _recipeData = recipe;
        }

        if(_recipeData == null)
        {
            Debug.LogError("Recipe data is null.");
            return;
        }

        _recipeNameText.text = _recipeData.Name;
        _resultItemIcon.SetItemData(_recipeData.Result, _recipeData.Amount);

        for (int i = 0; i < _recipeData.Ingredients.Length; i++)
        {
            if(i >= _ingredientItemIcons.Count)
            {
                CItemIconController newIcon = Instantiate(_ingredientItemIconPrefab, _ingredientItemIconLayout);
                _ingredientItemIcons.Add(newIcon);
            }

            _ingredientItemIcons[i].SetItemData(_recipeData.Ingredients[i].Item, _recipeData.Ingredients[i].Amount);
        }

    }

    private void OnEnable()
    {
        _craftButton.onClick.AddListener(OnCraftButtonClicked);
    }

    private void OnDisable()
    {
        _craftButton.onClick.RemoveAllListeners();
    }

    private void OnCraftButtonClicked()
    {
        CEnchantCraftingManager.Instance.CraftItem(_recipeData);
    }

    public void SetPanelActive(bool active)
    {
        if (active)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
        }

        else
        {
            _canvasGroup.alpha = 0.4f;
            _canvasGroup.interactable = false;
        }
    }
}
