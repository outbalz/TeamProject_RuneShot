using UnityEngine;

public class CProjectilePool : CObjectPool<CProjectilePool, CProjectile>
{
    #region 프로퍼티
    public CProjectile Prefab => _prefab;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        if (!enabled || Instance != this)
        {
            return;
        }

        Prewarm();
    }

    protected override void OnDestroy()
    {
        for (int i = 0; i < _liveList.Count; i++)
        {
            if (_liveList[i] != null)
            {
                Destroy(_liveList[i].gameObject);
            }
        }

        _liveList.Clear();

        while (_pool.Count > 0)
        {
            CProjectile projectile = _pool.Dequeue();

            if (projectile != null)
            {
                Destroy(projectile.gameObject);
            }
        }

        base.OnDestroy();
    }
}
