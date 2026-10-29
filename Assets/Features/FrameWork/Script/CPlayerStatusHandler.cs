using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerStatusHandler : CCharacterStatusHandler
{
    #region 내부변수
    private float _hunger;
    private float _maxHunger;
    private float _hungerRate = 1;
    #endregion


    #region 프로퍼티
    public override float HP 
    { 
        get { return GetStatusValue(_hp, "HP"); }
        set { _hp = value; } 
    }

    public override float MaxHP 
    { 
        get { return GetStatusValue(_maxHp, "MaxHP"); } 
        set { _maxHp = value; } 
    }

    public override float HealRate 
    { 
        get { return GetStatusValue(_healRate, "HealRate"); } 
        set { _healRate = value; } 
    }

    public override float MovementSpeed 
    { 
        get { return GetStatusValue(_movementSpeed, "MovementSpeed"); } 
        set { _movementSpeed = value; } 
    }

    public override float WeaponSpread 
    { 
        get { return GetStatusValue(_weaponSpread, "WeaponSpread"); } 
        set { _weaponSpread = value; } 
    }

    public override float WeaponSpreadHeapfire
    {
        get { return GetStatusValue(_weaponSpreadHeapfire, "WeaponSpreadHeapfire"); }
        set { _weaponSpreadHeapfire = value; }
    }

    public override float WeaponSpreadAimed
    {
        get { return GetStatusValue(_weaponSpreadAimed, "WeaponSpreadAimed"); }
        set { _weaponSpreadAimed = value; }
    }

    public override float WeaponFireInterval
    {
        get { return GetStatusValue(_weaponFireInterval, "WeaponFireInterval"); }
        set { _weaponFireInterval = value; }
    }

    public override float WeaponDamage 
    {
        get { return GetStatusValue(_weaponDamage, "WeaponDamage"); }
        set { _weaponDamage = value; }
    }

    public override float ReloadSpeed
    {
        get { return GetStatusValue(_reloadSpeed, "ReloadSpeed"); }
        set { _reloadSpeed = value; }
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
    #endregion

    private float GetStatusValue(float baseVal, string modifierName)
    {
        CModifierHandler.Instance.GetModifiedValue(modifierName, baseVal);

        return baseVal;
    }
}
