using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "RuneShot/Weapon/Shotgun Trigger")]
public class CShotgunTriggerStrategySO : CWeaponTriggerStrategySO
{
    #region 인스펙터
    [SerializeField, Min(1)] private int _pelletCount = 5;
    [SerializeField, Range(0.0f, 180.0f)] private float _spreadAngle = 40.0f;
    #endregion

    #region 프로퍼티
    public int PelletCount => _pelletCount;
    public float SpreadAngle => _spreadAngle;
    #endregion

    public override UniTask TriggerWeapon(CProjectileDataSO projectileData, CProjectilePool pool, Vector3 position, Vector2 direction, float damage, Transform shooter)
    {
        if (HasInvalidReferences(projectileData, pool, direction))
        {
            return UniTask.CompletedTask;
        }

        for (int i = 0; i < _pelletCount; i++)
        {
            float angle = 0.0f;

            if (_pelletCount > 1)
            {
                angle = -_spreadAngle * 0.5f + _spreadAngle * i / (_pelletCount - 1);
            }

            Vector2 pelletDirection = Quaternion.Euler(0.0f, 0.0f, angle) * direction.normalized;
            CProjectile projectile = pool.GetFromPool();
            projectile.Initialize(projectileData, pool, position, pelletDirection, damage, shooter);
        }

        return UniTask.CompletedTask;
    }
}
