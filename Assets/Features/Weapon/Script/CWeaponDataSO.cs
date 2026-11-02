using UnityEngine;

[CreateAssetMenu(menuName = "RuneShot/Weapon/Weapon Data")]
public class CWeaponDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] private CProjectileDataSO _projectile;
    [SerializeField] private CWeaponTriggerStrategySO _trigger;
    [SerializeField] private CWeaponReloadStrategySO _reload;
    #endregion

    #region 프로퍼티
    public CProjectileDataSO Projectile => _projectile;
    public CWeaponTriggerStrategySO Trigger => _trigger;
    #endregion
}
