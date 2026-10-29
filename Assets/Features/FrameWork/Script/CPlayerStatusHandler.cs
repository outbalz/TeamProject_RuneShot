using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerStatusHandler : CCharacterStatusHandler<CCharacterStatusDataSO>
{
    #region 인스팩터
    [SerializeField] private float _hunger;
    [SerializeField] private float _maxHunger;

    //==========vv멀티플라이어, 곱해서사용vv========
    [SerializeField] private float _hungerRate = 1;
    [SerializeField] private float _detectionDistance = 1;  //피탐지 거리
    //===========^^멀티플라이어^^================
    #endregion


    #region 프로퍼티
    public override float MaxHP 
    { 
        get { return GetStatusValue(_dataSO.MaxHP, "MaxHP"); } 
    }

    public override float HealRate 
    { 
        get { return GetStatusValue(_dataSO.HealRate, "HealRate"); } 
    }

    public override float MovementSpeed 
    { 
        get { return GetStatusValue(_dataSO.MovementSpeed, "MovementSpeed"); } 
    }

    public override float WeaponSpread 
    { 
        get { return GetStatusValue(_dataSO.WeaponSpread, "WeaponSpread"); }     }

    public override float WeaponSpreadHeapfire
    {
        get { return GetStatusValue(_dataSO.WeaponSpreadHeapfire, "WeaponSpreadHeapfire"); }
    }

    public override float WeaponSpreadAimed
    {
        get { return GetStatusValue(_dataSO.WeaponSpreadAimed, "WeaponSpreadAimed"); }
    }

    public override float WeaponFireInterval
    {
        get { return GetStatusValue(_dataSO.WeaponFireInterval, "WeaponFireInterval"); }
    }

    public override float WeaponDamage 
    {
        get { return GetStatusValue(_dataSO.WeaponDamage, "WeaponDamage"); }
    }

    public override float ReloadSpeed
    {
        get { return GetStatusValue(_dataSO.ReloadSpeed, "ReloadSpeed"); }
    }

    public float Hunger
    {
        get { return GetStatusValue(_hunger, "Hunger"); }
        set { _hunger = value; }
    }

    public float MaxHunger
    {
        get { return GetStatusValue(_maxHunger, "MaxHunger"); }
        set { _maxHunger = value; }
    }

    public float HungerRate
    {
        get { return GetStatusValue(_hungerRate, "HungerRate"); }
        set { _hungerRate = value; }
    }

    public float DetectionDistance
    {
        get { return GetStatusValue(_detectionDistance, "DetectionDistance"); }
        set { _detectionDistance = value; }
    }
    #endregion


    private float GetStatusValue(float baseVal, string modifierName)
    {
        CModifierHandler.Instance.GetModifiedValue(modifierName, baseVal);

        return baseVal;
    }
}
