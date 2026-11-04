using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EItemtype
{
	None,
	Weapon,
	Mics,
}

[CreateAssetMenu(menuName = "SO/ItemSO", fileName ="ItemSO_")]
public class CItemDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] private Sprite _itemSprite;
	[SerializeField] private int _maxStack;
	[SerializeField] private EItemtype _itemType;
	[SerializeReference, SubclassSelector] private CItemEffect _effect;
	[SerializeReference, SubclassSelector] private CCondition _condition;
	#endregion

	#region 프로퍼티
	public Sprite ItemSprite => _itemSprite;
	public int MaxStack => _maxStack;
	public EItemtype ItemType => _itemType;
	#endregion

	public void OnUse()
	{
		if (_condition != null && !_condition.CheckCondition())
		{
			return;
		}

		_effect.OnUse().Forget();
	}

    protected override void OnValidate()
    {
        base.OnValidate();

		if(_maxStack < 1)
		{
			_maxStack = 1;
		}
    }
}


public abstract class CItemEffect
{
	public abstract UniTask OnUse();
}