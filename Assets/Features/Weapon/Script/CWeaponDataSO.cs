
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponDataSO : CGameDataSO
{
	#region 인스팩터
	[SerializeField] CProjectileDataSO _projectile;
	[SerializeField] CWeaponTriggerStrategySO _trigger;
	[SerializeField] CWeaponReloadStrategySO _reload;
	#endregion

}
