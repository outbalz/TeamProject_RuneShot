using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkillDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] protected List<CAddModifier> _modifiers;
    #endregion

    protected virtual void OnSkiilGet()
    {
        CModifierHandler modifierHandler = CModifierHandler.Instance;

        for (int i = 0; i < _modifiers.Count; i++)
        {
            modifierHandler.ApplyModifierWithCheck(_modifiers[i]);
        }
    }

    protected virtual void OnSkiilLost()
    {
        CModifierHandler modifierHandler = CModifierHandler.Instance;

        for (int i = 0; i < _modifiers.Count; i++)
        {
            modifierHandler.RemoveModifier(_modifiers[i]);
        }
    }
}
