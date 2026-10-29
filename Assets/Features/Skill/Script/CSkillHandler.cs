

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkillHandler : CSingleton<CSkillHandler>
{
	#region 내부변수
	private List<CSkillDataSO> _skills = new List<CSkillDataSO>();
	#endregion

	public void ApllySkill(CSkillDataSO skill)
	{
		_skills.Add(skill);
		skill.OnSkiilGet();
	}

	public void RemoveSkill(CSkillDataSO skill)
	{
		_skills.Remove(skill);
		skill.OnSkiilLost();
	}

	/*//Test:======================================
	[SerializeField] private CSkillDataSO _skill;

	[ContextMenu("ApplySkillTest")]
	private void ApplySkiilTest()
	{
		ApllySkill(_skill);
	}

    [ContextMenu("RemoveSkillTest")]
    private void RemoveSkiilTest()
    {
        RemoveSkill(_skill);
    }
    [ContextMenu("PrintTestData")]
	private void PrintTestData()
	{
		Debug.Log($"{CModifierHandler.Instance.GetModifiedValue("WeaponDamage", 1)}");
	}
	//Test:======================================*/

}
