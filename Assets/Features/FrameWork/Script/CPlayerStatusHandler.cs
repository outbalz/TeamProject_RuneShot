using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerStatusHandler : CCharacterStatusHandler<CPlayerStausDataSO>
{
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

    public float MaxHunger
    {
        get { return GetStatusValue(_dataSO.MaxHunger, "MaxHunger"); }
    }

    public float HungerRate
    {
        get { return GetStatusValue(_dataSO.HungerRate, "HungerRate"); }
    }

    public float DetectionDistance
    {
        get { return GetStatusValue(_dataSO.DetectionDistance, "DetectionDistance"); }
    }
    #endregion


    private float GetStatusValue(float baseVal, string modifierName)
    {
        float result = baseVal;
        result = CModifierHandler.Instance.GetModifiedValue(modifierName, baseVal);

        return result;
    }
}
