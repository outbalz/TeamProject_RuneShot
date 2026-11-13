using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHPHandler
{
    float HP { get; set; }
    float MaxHP { get; }
    event Action OnHitEvent;
    event Action OnDeath;
    UniTask OnHit(float damage);
    UniTask OnHeal(float amount);
}

public abstract class CHPHandler<T> : MonoBehaviour, IHPHandler where T : CCharacterStatusDataSO
{
    #region 인스팩터
    [SerializeField] protected CCharacterStatusHandler<T> _status;
    #endregion

    #region 내부변수
    protected float _hp;
    #endregion

    #region 프로퍼티
    public float HP { get { return _hp; } set { _hp = Mathf.Clamp(value, 0, _status.MaxHP); } }
    public float MaxHP => _status.MaxHP;
    public event Action OnHitEvent;
    public event Action OnDeath;
    #endregion

    public abstract UniTask OnHit(float damage);
    public abstract UniTask OnHeal(float amount);

    public void OnDeathInvoke()
    {
        OnDeath?.Invoke();
    }

    public void OnHitInvoke()
    {
        OnHitEvent?.Invoke();
    }

}
