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
public class CPlayerHPCondition : CCondition
{
    [SerializeField] private float _percentage;

    public override bool CheckCondition()
    {
        CPlayerHPHandler playerHP = CPlayerCharacterController.Instance.HPhandler;

        if(playerHP == null)
        {
            return false;
        }

        if (playerHP.HP <= playerHP.MaxHP * _percentage)
        {
            return true;
        }

        return false;
    }
}


