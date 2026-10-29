using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class CWeaponReloadStrategySO : CGameDataSO
{
    public abstract UniTask ReloadWeapon();
}
