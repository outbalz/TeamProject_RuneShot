using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "SO/SkillDataSO", fileName = "SkillDataSO_")]
public abstract class CSkillDataSO<T> : CGameDataSO where T : CModifier
{
    #region 인스팩터
    [SerializeField] protected List<T> _modifiers;
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


[CreateAssetMenu(menuName = "SO/SkillDataSO/AddModifier", fileName = "SkillAddModifierDataSO_")]
public class CSkillAddModifierDataSO : CSkillDataSO<CAddModifier>
{

}

[CreateAssetMenu(menuName = "SO/SkillDataSO/MuliflyModifier", fileName = "SkillMuliflyModifierDataSO_")]
public class CSkillMuliflyModifierDataSO : CSkillDataSO<CMultiflyModifier>
{

}