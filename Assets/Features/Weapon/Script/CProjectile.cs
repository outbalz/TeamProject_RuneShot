using UnityEngine;
using Cysharp.Threading.Tasks;

public class CProjectile : MonoBehaviour
{
    #region 인스펙터
    [SerializeField, Min(0.01f)] private float _collisionRadius = 0.05f;
    #endregion

    #region 내부 변수
    private CProjectilePool _pool;
    private CProjectileDataSO _data;
    private Transform _shooter;
    private bool _isEnemyShot;
    private Vector2 _direction;
    private float _speed;
    private float _damage;
    private float _remainingLifeTime;
    private bool _isReturned = true;
    #endregion

    public void Initialize(CProjectileDataSO data, CProjectilePool pool, Vector3 position, Vector2 direction)
    {
        Initialize(data, pool, position, direction, data.Damage, null);
    }

    public void Initialize(CProjectileDataSO data, CProjectilePool pool, Vector3 position, Vector2 direction, float damage)
    {
        Initialize(data, pool, position, direction, damage, null);
    }

    public void Initialize(CProjectileDataSO data, CProjectilePool pool, Vector3 position, Vector2 direction, float damage, Transform shooter)
    {
        _shooter = shooter;
        _isEnemyShot = shooter != null && shooter.GetComponentInParent<CEnemyStatusHandler>() != null;
        _pool = pool;
        _data = data;
        _direction = direction.normalized;
        _speed = data.Speed;
        _damage = damage;
        _remainingLifeTime = data.LifeTime;
        _isReturned = false;

        transform.SetParent(null);
        transform.position = position;
        transform.right = _direction;
    }

    private void Update()
    {
        if (_isReturned)
        {
            return;
        }

        float moveTime = Mathf.Min(Time.deltaTime, _remainingLifeTime);
        float distance = _speed * moveTime;

        if (TryHit(distance))
        {
            return;
        }

        transform.position += (Vector3)(_direction * distance);
        _remainingLifeTime -= Time.deltaTime;

        if (_remainingLifeTime <= 0.0f)
        {
            ReturnToPool();
        }
    }

    private bool TryHit(float distance)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _collisionRadius, _direction, distance);

        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D collider = hits[i].collider;

            if (collider.isTrigger || (_shooter != null && collider.transform.IsChildOf(_shooter)))
            {
                continue;
            }

            CEnemyHPHandler enemyHP = collider.GetComponentInParent<CEnemyHPHandler>();
            CPlayerHPHandler playerHP = collider.GetComponentInParent<CPlayerHPHandler>();

            if (_isEnemyShot)
            {
                if (enemyHP != null || collider.GetComponentInParent<CEnemyStatusHandler>() != null ||
                    collider.GetComponentInParent<CEnemy>() != null)
                {
                    continue;
                }
            }
            else if (playerHP != null || collider.GetComponentInParent<CPlayerStateController>() != null)
            {
                continue;
            }

            CProjectileDataSO data = _data;
            float damage = _damage;
            transform.position = hits[i].centroid;

            ReturnToPool();

            if (playerHP != null)
            {
                playerHP.OnHit(damage).Forget();
            }
            else if (enemyHP != null)
            {
                enemyHP.OnHit(damage).Forget();
            }

            if (playerHP != null || enemyHP != null)
            {
                data.OnHit().Forget();
            }
            else
            {
                data.OnImpact().Forget();
            }

            return true;
        }

        return false;
    }

    public void ReturnToPool()
    {
        if (_isReturned || _pool == null)
        {
            return;
        }

        _isReturned = true;
        _data = null;
        _shooter = null;
        _isEnemyShot = false;
        _remainingLifeTime = 0.0f;
        _speed = 0.0f;
        _damage = 0.0f;
        _direction = Vector2.zero;
        _pool.ReturnToPool(this);
    }
}
