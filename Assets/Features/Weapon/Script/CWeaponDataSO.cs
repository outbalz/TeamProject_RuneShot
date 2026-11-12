using UnityEngine;

[CreateAssetMenu(menuName = "RuneShot/Weapon/Weapon Data")]
public class CWeaponDataSO : CGameDataSO
{
    #region 인스팩터
    [SerializeField] private CProjectileDataSO _projectile;
    [SerializeField] private CWeaponTriggerStrategySO _trigger;
    [SerializeField] private CWeaponReloadStrategySO _reload;
    [SerializeField, Min(0.0f)] private float _fireInterval;
    [SerializeField, Min(1)] private int _magazineSize = 12;
    [SerializeField, Min(0.01f)] private float _reloadTime = 1.0f;
    [SerializeField] private CItemDataSO _ammoItem;
    #endregion

    #region 프로퍼티
    public CProjectileDataSO Projectile => _projectile;
    public CWeaponTriggerStrategySO Trigger => _trigger;
    public float FireInterval => _fireInterval;
    public int MagazineSize => Mathf.Max(1, _magazineSize);
    public float ReloadTime => _reloadTime;
    public CItemDataSO AmmoItem => _ammoItem;
    #endregion
}
