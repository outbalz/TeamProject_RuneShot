using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/ItemSO", fileName ="ItemSO_")]
public abstract class CItemDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] private Sprite _itemSprite;
	[SerializeField] private float _itemPrice;
	#endregion

	#region 프로퍼티
	public Sprite ItemSprite => _itemSprite;
	public float ItemPrice => _itemPrice;
	#endregion

	public abstract void OnUse(GameObject target);
}
