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
	Consumable,
	Mics,
}

[CreateAssetMenu(menuName = "SO/ItemSO", fileName ="ItemSO_")]
public class CItemDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] private Sprite _itemSprite;
	[SerializeField] private int _maxStack;
	[SerializeField] private EItemtype _itemType;
	[SerializeReference, SubclassSelector] private CItemEffect[] _effect;
	[SerializeReference, SubclassSelector] private CCondition[] _condition;
	#endregion

	#region 프로퍼티
	public Sprite ItemSprite => _itemSprite;
	public int MaxStack => _maxStack;
	public EItemtype ItemType => _itemType;
	#endregion

	public void OnUse()
	{
		for (int i = 0; i < _condition.Length; i++)
		{
			if(_condition[i] == null)
			{
				continue;
            }

            if (!_condition[i].CheckCondition())
			{
				return;
			}
        }

        for (int i = 0; i < _effect.Length; i++)
		{
			_effect[i].OnUse().Forget();
        }
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

[Serializable]
public abstract class CItemEffect
{
	public abstract UniTask OnUse();
}

[Serializable]
public class CItemEffectHeal : CItemEffect
{
	#region 인스팩터
	[SerializeField] private float _healAmount;
	#endregion
	public override UniTask OnUse()
	{
		CPlayerHPHandler playerHP = CPlayerCharacterController.Instance.HPhandler;
		if(playerHP == null)
		{
			return UniTask.CompletedTask;
		}

		playerHP.OnHeal(_healAmount);
		return UniTask.CompletedTask;
    }
}

[Serializable]
public class CItemEffectHealOverTime : CItemEffect
{
	#region 인스팩터
	[SerializeField] private float _healAmount;
	[SerializeField] private float _duration;
	#endregion
	public override async UniTask OnUse()
	{
		CPlayerHPHandler playerHP = CPlayerCharacterController.Instance.HPhandler;
		if(playerHP == null)
		{
			return;
		}

		float healPerSecond = _healAmount / _duration;
		float timer = 0.0f;

		while(timer < _duration)
		{
			await playerHP.OnHeal(healPerSecond * Time.deltaTime);
			timer += Time.deltaTime;
			await UniTask.Yield();
		}
	}
}