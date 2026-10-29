using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillDataSO", fileName = "SkillDataSO_")]
public class CSkillDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] protected List<CAddModifier> _modifiers;
    #endregion

    public virtual void OnSkiilGet()
    {
        CModifierHandler modifierHandler = CModifierHandler.Instance;

        for (int i = 0; i < _modifiers.Count; i++)
        {
            modifierHandler.ApplyModifierWithCheck(_modifiers[i]);
        }
    }

    public virtual void OnSkiilLost()
    {
        CModifierHandler modifierHandler = CModifierHandler.Instance;

        for (int i = 0; i < _modifiers.Count; i++)
        {
            modifierHandler.RemoveModifier(_modifiers[i]);
        }
    }
}
