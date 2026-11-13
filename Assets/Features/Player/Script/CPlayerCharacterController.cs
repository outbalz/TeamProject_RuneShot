using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCharacterController : CSingleton<CPlayerCharacterController>
{
	#region 인스팩터
	[SerializeField] private CPlayerInteractionController _interactionController;
	[SerializeField] private CPlayerHPHandler _hpHandler;
	[SerializeField] private CPlayerStatusHandler _statusHandler;
	[SerializeField] private CStatusEffectHandler _statusEffectHandler;
    [SerializeField] private CModifierHandler _modifierHandler;
	[SerializeField] private CWeapon _weapon;
	#endregion

	#region 프로퍼티
	public CPlayerInteractionController InteractionController => _interactionController;
	public CPlayerHPHandler HPhandler => _hpHandler;
	public CPlayerStatusHandler Status => _statusHandler;
	public CStatusEffectHandler StatusEffectHandler => _statusEffectHandler;
    public CModifierHandler ModifierHandler => _modifierHandler;
	public CWeapon Weapon => _weapon;
	#endregion
}
