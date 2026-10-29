using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CHPHandler : MonoBehaviour
{
    #region 인스팩터
    [SerializeField] protected CCharacterStatusHandler<CCharacterStatusDataSO> _status;
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
