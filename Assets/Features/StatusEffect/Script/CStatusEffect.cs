using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum EStatusEffectStackType
{
    None,
    Stackable,
    Refreshable,
    Extendable,
}

[System.Serializable]
public abstract class CStatusEffect
{
    #region 인스팩터
    [SerializeField] protected string _effectName;
    [SerializeField] protected EStatusEffectStackType _stackType;
    [SerializeField] protected float _duration;
    #endregion

    #region 내부변수
    protected CancellationTokenSource _cts;
    protected float _durationTimer;
    protected CStatusEffectHandler _statusEffectHandler;
    #endregion

    #region 프로퍼티
    public string EffectName => _effectName;
    public EStatusEffectStackType StackType => _stackType;
    public float Duration => _duration;
    public float DurationTimer { get { return _durationTimer; } set { _durationTimer = value; } }
    #endregion

    public virtual void ApplyEffect<T>(T target, CStatusEffectHandler statusEffectHandler)
    {
        _statusEffectHandler = statusEffectHandler;

        _cts = new CancellationTokenSource();

        _durationTimer = _duration;

        Effect<T>(target, _cts.Token).Forget();
    }


    public virtual void CancelEffect()
    {
        if(_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }

    public virtual void RemoveEffect(CStatusEffectHandler statusEffectHandler)
    {
        CancelEffect();
        statusEffectHandler.RemoveStatusEffect(this);
    }

    public abstract UniTask Effect<T>(T target, CancellationToken ct);
}

[System.Serializable]
public class CStatusEffectHeal : CStatusEffect
{
    public override async UniTask Effect<T>(T target, CancellationToken ct)
    {
        if(target is not IHPHandler hp)
        {
            Debug.LogError("타겟이 IHPHandler가 아닙니다.");
            return;
        }

        while (_durationTimer > 0)
        {
            if (ct.IsCancellationRequested)
            {
                break;
            }

            hp.OnHeal(1f).Forget();

            _durationTimer -= Time.deltaTime;

            await UniTask.Yield(ct);
        }

        RemoveEffect(_statusEffectHandler);
    }
}

[System.Serializable]
public class CStatusEffectDamage : CStatusEffect
{
    public override async UniTask Effect<T>(T target, CancellationToken ct)
    {
        if(target is not IHPHandler hp)
        {
            Debug.LogError("타겟이 IHPHandler가 아닙니다.");
            return;
        }

        while (_durationTimer > 0)
        {
            if (ct.IsCancellationRequested)
            {
                break;
            }

            hp.OnHit(1f).Forget();

            _durationTimer -= Time.deltaTime;

            await UniTask.Yield(ct);
        }

        RemoveEffect(_statusEffectHandler);
    }
}

[System.Serializable]
public class CStatusEffectApplyModifier : CStatusEffect
{
    [SerializeReference, SubclassSelector] private CModifier _modifier;

    public override async UniTask Effect<T>(T target, CancellationToken ct)
    {
        if(target is not CModifierHandler modifierHandler)
        {
            Debug.LogError("타겟이 CModifierHandler가 아닙니다.");
            return;
        }

        modifierHandler.ApplyModifierWithCheck(_modifier);

        while (_durationTimer > 0)
        {
            if (ct.IsCancellationRequested)
            {
                break;
            }

            _durationTimer -= Time.deltaTime;
            await UniTask.Yield(ct);
        }

        modifierHandler.RemoveModifier(_modifier);

        RemoveEffect(_statusEffectHandler);
    }
}