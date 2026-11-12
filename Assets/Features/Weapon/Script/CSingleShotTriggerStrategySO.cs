using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "RuneShot/Weapon/Single Shot Trigger")]
public class CSingleShotTriggerStrategySO : CWeaponTriggerStrategySO
{
    public override UniTask TriggerWeapon(CProjectileDataSO projectileData, CProjectilePool pool, Vector3 position, Vector2 direction, float damage)
    {
        if (HasInvalidReferences(projectileData, pool, direction))
        {
            return UniTask.CompletedTask;
        }

        CProjectile projectile = pool.GetFromPool();
        projectile.Initialize(projectileData, pool, position, direction, damage);

        return UniTask.CompletedTask;
    }
}
