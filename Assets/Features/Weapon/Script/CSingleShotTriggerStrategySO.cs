using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "RuneShot/Weapon/Single Shot Trigger")]
public class CSingleShotTriggerStrategySO : CWeaponTriggerStrategySO
{
    public override UniTask TriggerWeapon(CProjectileDataSO projectileData, CProjectilePool pool, Vector3 position, Vector2 direction)
    {
        if (projectileData == null || projectileData.Prefab == null || pool == null ||
            !pool.isActiveAndEnabled || pool.Prefab != projectileData.Prefab || direction == Vector2.zero)
        {
            Debug.LogError("투사체 데이터, 풀, 프리팹 또는 발사 방향을 확인하세요.");
            return UniTask.CompletedTask;
        }

        CProjectile projectile = pool.GetFromPool();
        projectile.Initialize(projectileData, pool, position, direction);

        return UniTask.CompletedTask;
    }
}
