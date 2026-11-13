using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillDataSO", fileName = "SkillDataSO_")]
public class CSkillDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeReference, SubclassSelector] protected List<CModifier> _modifiers;
    #endregion

    public virtual void OnSkiilGet()
    {
        CModifierHandler modifierHandler = CPlayerCharacterController.Instance.ModifierHandler;

        for (int i = 0; i < _modifiers.Count; i++)
        {
            modifierHandler.ApplyModifierWithCheck(_modifiers[i]);
        }
    }

    public virtual void OnSkiilLost()
    {
        CModifierHandler modifierHandler = CPlayerCharacterController.Instance.ModifierHandler;

        for (int i = 0; i < _modifiers.Count; i++)
        {
            modifierHandler.RemoveModifier(_modifiers[i]);
        }
    }
}
