using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerStatusHandler : CCharacterStatusHandler<CPlayerStausDataSO>
{
    #region 프로퍼티
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


    protected override float GetStatusValue(float baseVal, string modifierName)
    {
        _modifierHandler = CPlayerCharacterController.Instance.ModifierHandler;

        return base.GetStatusValue(baseVal, modifierName);
    }
}
