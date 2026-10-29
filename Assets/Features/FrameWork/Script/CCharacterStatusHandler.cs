using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterStatusHandler<T> : MonoBehaviour where T : CCharacterStatusDataSO
{
    #region 인스팩터
    [SerializeField] protected T _dataSO;
    #endregion

    #region 프로퍼티
    public virtual float MaxHP => _dataSO.MaxHP;
    public virtual float HealRate => _dataSO.HealRate;
    public virtual float MovementSpeed => _dataSO.MovementSpeed;
    public virtual float WeaponSpread => _dataSO.WeaponSpread;
    public virtual float WeaponSpreadHeapfire => _dataSO.WeaponSpreadHeapfire;
    public virtual float WeaponSpreadAimed => _dataSO.WeaponSpreadAimed;
    public virtual float WeaponFireInterval => _dataSO.WeaponFireInterval;
    public virtual float WeaponDamage => _dataSO.WeaponDamage;
    public virtual float ReloadSpeed => _dataSO.ReloadSpeed;
    #endregion

    private void Awake()
    {
        if (_dataSO == null)
        {
            Debug.LogWarning("에너미 스탯 데이터 SO 누락");
            enabled = false;
            return;
        }
    }

}
