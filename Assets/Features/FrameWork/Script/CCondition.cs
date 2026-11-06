using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CCondition
{
    public abstract bool CheckCondition();
}

[Serializable]
public class CPlayerHPLesserCondition : CCondition
{
    [Range(0,1)]
    [SerializeField] private float _percentage;
    [SerializeField] private bool _allowEqual;

    public override bool CheckCondition()
    {
        CPlayerHPHandler playerHP = CPlayerCharacterController.Instance.HPhandler;

        if(playerHP == null)
        {
            return false;
        }

        if(_allowEqual && playerHP.HP == playerHP.MaxHP * _percentage)
        {
            return true;
        }

        if (playerHP.HP < playerHP.MaxHP * _percentage)
        {
            return true;
        }

        return false;
    }
}

[Serializable]
public class CPlayerHPGreaterCondition : CCondition
{
    [Range(0, 1)]
    [SerializeField] private float _percentage;
    [SerializeField] private bool _allowEqual;

    public override bool CheckCondition()
    {
        CPlayerHPHandler playerHP = CPlayerCharacterController.Instance.HPhandler;

        if(playerHP == null)
        {
            return false;
        }

        if(_allowEqual && playerHP.HP == playerHP.MaxHP * _percentage)
        {
            return true;
        }

        if (playerHP.HP > playerHP.MaxHP * _percentage)
        {
            return true;
        }

        return false;
    }
}

[Serializable]
public class CNotUseableCondition : CCondition
{
    public override bool CheckCondition()
    {
        return false;
    }
}
