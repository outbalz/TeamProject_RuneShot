using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CModifier
{
	#region 인스팩터
	[SerializeField] protected float _modifier;
    [SerializeField] protected string _modifierName;
    #endregion

    #region 내부 변수
    private CModifierHandler _modifierHandler;
    #endregion

    #region 프로퍼티
    public CModifierHandler ModifierHandler { get { return _modifierHandler; } set { _modifierHandler = value; } }  
    public string ModifierName => _modifierName;
    public float ModifierValue => _modifier;
    #endregion


	public abstract float GetModifiedValue(float baseValue);

    public CModifier(string name,float modifier) 
    {
        _modifierName = name;
        _modifier = modifier;
    }
}

[Serializable]
public class CAddModifier : CModifier
{

    public override float GetModifiedValue(float baseValue)
    {
		return baseValue + _modifier;
    }

    public CAddModifier(string name, float modifier) : base(name, modifier)
    {
    }

    public CAddModifier() : this("", 1)
    {

    }
}


[Serializable]
public class CMultiflyModifier : CModifier
{

    public override float GetModifiedValue(float baseValue)
    {
        return baseValue * _modifier;
    }    
    
    public CMultiflyModifier(string name, float modifier) : base(name, modifier)
    {
    }

    public CMultiflyModifier() : this("", 1)
    {

    }
}

[Serializable]
public class CAddtiveMultiflyModifier : CModifier
{
    #region 인스팩터
    [SerializeField] private string _addModifierName;
    #endregion

    public override float GetModifiedValue(float baseValue)
    {
        if (ModifierHandler == null)
        {
            return baseValue * _modifier;
        }

        return baseValue * ModifierHandler.GetModifiedValue(_addModifierName, _modifier);
    }

    public CAddtiveMultiflyModifier(string name, string addModifierName, float modifier, CModifierHandler modifierHandler) : base(name, modifier)
    {
        _addModifierName = addModifierName;
        ModifierHandler = modifierHandler;
    }
}


[Serializable]
public class CConditionalModifier : CModifier
{
    [SerializeReference, SubclassSelector] private CCondition _condition;
    [SerializeReference, SubclassSelector] private CModifier _tModifier;

    public override float GetModifiedValue(float baseValue)
    {
        if (_condition.CheckCondition())
        {
            return _tModifier.GetModifiedValue(baseValue);
        }

        return baseValue * _modifier;
    }

    public CConditionalModifier(string name, float modifier = 1f) : base(name, modifier)
    {
    }

    public CConditionalModifier() : this("")
    {
    }

}
