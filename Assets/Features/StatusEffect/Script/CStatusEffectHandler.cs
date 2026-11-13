using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CStatusEffectHandler : MonoBehaviour
{
    #region 인스팩터
    [SerializeField] private List<CStatusEffect> _statusEffects;
    #endregion

    #region 내부변수
    private Dictionary<string, CStatusEffect> _effectsMap = new Dictionary<string, CStatusEffect>();
    #endregion

    #region 프로퍼티
    public IReadOnlyList<CStatusEffect> StatusEffects => _statusEffects;
    public IReadOnlyDictionary<string, CStatusEffect> EffectsMap => _effectsMap;
    #endregion

    public void ApplyStatusEffect<T>(CStatusEffect effect, T target)
    {
        if(!_effectsMap.ContainsKey(effect.EffectName))
        {
            _effectsMap.Add(effect.EffectName, effect);
            _statusEffects.Add(effect);
            effect.ApplyEffect(target, this);
        }

        else
        {
            switch (effect.StackType)
            {
                case EStatusEffectStackType.None:
                    return;
                case EStatusEffectStackType.Stackable:
                    _statusEffects.Add(effect);
                    break;
                case EStatusEffectStackType.Refreshable:
                    _effectsMap[effect.EffectName].CancelEffect();
                    _effectsMap[effect.EffectName].ApplyEffect(target, this);
                    break;
                case EStatusEffectStackType.Extendable:
                    _effectsMap[effect.EffectName].DurationTimer += effect.Duration;
                    break;
                default:
                    break;
            }
        }
    }

    public void RemoveStatusEffect(CStatusEffect effect)
    {
        if(!_effectsMap.ContainsKey(effect.EffectName))
        {
            Debug.LogWarning($"Effect {effect.EffectName} not found in the effects map.");
            return;
        }

        effect.CancelEffect();
        _statusEffects.Remove(effect);

        if (_effectsMap[effect.EffectName].StackType == EStatusEffectStackType.Stackable)
        {
            CStatusEffect nextEffect = _statusEffects.Find(e => e.EffectName == effect.EffectName);

            if(nextEffect == null)
            {
                _effectsMap.Remove(effect.EffectName);
                return;
            }

            if(nextEffect != _effectsMap[effect.EffectName])
            {
                _effectsMap[effect.EffectName] = nextEffect;
            }

            return;
        }

        _effectsMap.Remove(effect.EffectName);
    }
}
