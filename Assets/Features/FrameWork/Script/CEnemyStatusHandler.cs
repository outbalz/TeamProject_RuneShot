using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyStatusHandler : CCharacterStatusHandler<CEnemyStatusDataSO>
{
    #region 프로퍼티
    public float DetectionDistance
    {
        get { return GetStatusValue(_dataSO.DetectionDistance, "DetectionDistance"); }
    }

    public float AttackRange
    {
        get { return GetStatusValue(_dataSO.AttackRange, "AttackRange"); }
    }
    #endregion

    protected override float GetStatusValue(float baseVal, string modifierName)
    {
        if (_modifierHandler == null)
        {
            if(!TryGetComponent<CModifierHandler>(out _modifierHandler))
            {
                return baseVal;
            }
        }

        return base.GetStatusValue(baseVal, modifierName);
    }
}
