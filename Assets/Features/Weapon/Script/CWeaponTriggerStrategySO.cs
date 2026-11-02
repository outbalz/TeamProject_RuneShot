using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class CWeaponTriggerStrategySO : CGameDataSO
{
    public abstract UniTask TriggerWeapon(CProjectileDataSO projectileData, CProjectilePool pool, Vector3 position, Vector2 direction);
}
