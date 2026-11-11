using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CCharacterStatusHandler<T> : MonoBehaviour where T : CCharacterStatusDataSO
{
    #region 인스팩터
    [SerializeField] protected T _dataSO;
    [SerializeField] protected CModifierHandler _modifierHandler;
    #endregion

    #region 프로퍼티
    public event Action OnStatChange;
    public virtual float MaxHP
    {
        get { return GetStatusValue(_dataSO.MaxHP, "MaxHP"); }
    }

    public virtual float HealRate
    {
        get { return GetStatusValue(_dataSO.HealRate, "HealRate"); }
    }

    public virtual float MovementSpeed
    {
        get { return GetStatusValue(_dataSO.MovementSpeed, "MovementSpeed"); }
    }

    public virtual float WeaponSpread
    {
        get { return GetStatusValue(_dataSO.WeaponSpread, "WeaponSpread"); }
    }

    public virtual float WeaponSpreadHeapfire
    {
        get { return GetStatusValue(_dataSO.WeaponSpreadHeapfire, "WeaponSpreadHeapfire"); }
    }

    public virtual float WeaponSpreadAimed
    {
        get { return GetStatusValue(_dataSO.WeaponSpreadAimed, "WeaponSpreadAimed"); }
    }

    public virtual float WeaponFireInterval
    {
        get { return GetStatusValue(_dataSO.WeaponFireInterval, "WeaponFireInterval"); }
    }

    public virtual float WeaponDamage
    {
        get { return GetStatusValue(_dataSO.WeaponDamage, "WeaponDamage"); }
    }

    public virtual float ReloadSpeed
    {
        get { return GetStatusValue(_dataSO.ReloadSpeed, "ReloadSpeed"); }
    }
    #endregion

    protected virtual void Awake()
    {
        if (_dataSO == null)
        {
            Debug.LogWarning("스탯 데이터 SO 누락");
            enabled = false;
            return;
        }
    }

    protected virtual float GetStatusValue(float baseVal, string modifierName)
    {
        float result = baseVal;
        result = _modifierHandler.GetModifiedValue(modifierName, baseVal);

        return result;
    }

    public void InvokeOnStatChange()
    {
        OnStatChange?.Invoke();
    }

}
