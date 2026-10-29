using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterStatusHandler : MonoBehaviour
{
    #region 내부변수
    protected float _hp;
    protected float _maxHp;
    protected float _healRate = 1;
    protected float _movementSpeed;
    
    //탄퍼짐
    protected float _weaponSpread = 1;
    //지향사격 탄퍼짐
    protected float _weaponSpreadHeapfire = 1.4f;
    //조준사격 탄퍼짐
    protected float _weaponSpreadAimed = 0.6f;
    
    //연사 지연시간
    protected float _weaponFireInterval = 1;

    protected float _weaponDamage = 1;
    protected float _reloadSpeed = 1;
    #endregion

    #region 프로퍼티
    public virtual float HP { get { return _hp; } set { _hp = value; } }
    public virtual float MaxHP { get { return _maxHp; } set { _maxHp = value; } }
    public virtual float HealRate { get { return _healRate; } set { _healRate = value;  } }
    public virtual float MovementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }
    public virtual float WeaponSpread { get { return _weaponSpread; } set { _weaponSpread = value; } }
    public virtual float WeaponSpreadHeapfire { get { return _weaponSpreadHeapfire; } set { _weaponSpreadHeapfire = value; } }
    public virtual float WeaponSpreadAimed { get { return _weaponSpreadAimed; } set { _weaponSpreadAimed = value; } }
    public virtual float WeaponFireInterval { get { return _weaponFireInterval; } set { _weaponFireInterval = value; } }
    public virtual float WeaponDamage { get { return _weaponDamage; } set { _weaponDamage = value; } }
    public virtual float ReloadSpeed { get { return _reloadSpeed; } set { _reloadSpeed = value; } }
    #endregion

}
