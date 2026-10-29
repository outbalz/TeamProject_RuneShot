using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterStatusDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _movementSpeed;

    //==========vv멀티플라이어, 곱해서사용vv========
    [SerializeField] protected float _healRate = 1;
    [SerializeField] protected float _weaponSpread = 1;    //탄퍼짐
    [SerializeField] protected float _weaponSpreadHeapfire = 1.4f;    //지향사격 탄퍼짐
    [SerializeField] protected float _weaponSpreadAimed = 0.6f;    //조준사격 탄퍼짐
    [SerializeField] protected float _weaponFireInterval = 1;    //연사 지연시간
    [SerializeField] protected float _weaponDamage = 1;
    [SerializeField] protected float _reloadSpeed = 1;
    //===========^^멀티플라이어^^================
    #endregion

    #region 프로퍼티
    public float MaxHP => _maxHp;
    public float MovementSpeed => _movementSpeed;
    public float HealRate => _healRate;
    public float WeaponSpread => _weaponSpread;
    public float WeaponSpreadHeapfire => _weaponSpreadHeapfire;
    public float WeaponSpreadAimed => _weaponSpreadAimed;
    public float WeaponFireInterval => _weaponFireInterval;
    public float WeaponDamage => _weaponDamage;
    public float ReloadSpeed => _reloadSpeed;
    #endregion
}
