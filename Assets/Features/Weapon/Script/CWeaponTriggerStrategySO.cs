using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class CWeaponTriggerStrategySO : CGameDataSO
{
    public virtual bool IsAutomatic => false;

    protected bool HasInvalidReferences(CProjectileDataSO projectileData, CProjectilePool pool, Vector2 direction)
    {
        if (projectileData == null || projectileData.Prefab == null || pool == null ||
            !pool.isActiveAndEnabled || pool.Prefab != projectileData.Prefab || direction == Vector2.zero)
        {
            Debug.LogError("투사체 데이터, 풀, 프리팹 또는 발사 방향을 확인하세요.");
            return true;
        }

        return false;
    }

    public UniTask TriggerWeapon(CProjectileDataSO projectileData, CProjectilePool pool, Vector3 position, Vector2 direction)
    {
        if (HasInvalidReferences(projectileData, pool, direction))
        {
            return UniTask.CompletedTask;
        }

        return TriggerWeapon(projectileData, pool, position, direction, projectileData.Damage, null);
    }

    public UniTask TriggerWeapon(CProjectileDataSO projectileData, CProjectilePool pool, Vector3 position, Vector2 direction, float damage)
    {
        return TriggerWeapon(projectileData, pool, position, direction, damage, null);
    }

    public abstract UniTask TriggerWeapon(CProjectileDataSO projectileData, CProjectilePool pool, Vector3 position, Vector2 direction, float damage, Transform shooter);
}
