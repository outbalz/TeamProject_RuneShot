using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CItemIconController : MonoBehaviour
{
	#region 인스팩터
	[SerializeField] private Image _itemIconImage;
	[SerializeField] private TMP_Text _itemAmountText;
	[SerializeField] private CItemDataSO _itemData;
    #endregion

	public void SetItemData(CItemDataSO itemData, int amount)
	{
		if(itemData != null)
		{
			_itemData = itemData;
        }

		_itemIconImage.sprite = _itemData.ItemSprite;

        _itemAmountText.text = amount.ToString();

		if(amount <= 1)
		{
			_itemAmountText.gameObject.SetActive(false);
		}

		else
		{
			_itemAmountText.gameObject.SetActive(true);
        }
    }
}
