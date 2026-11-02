using UnityEngine;

public class CProjectile : MonoBehaviour
{
    #region 내부 변수
    private CProjectilePool _pool;
    private Vector2 _direction;
    private float _speed;
    private float _remainingLifeTime;
    private bool _isReturned = true;
    #endregion

    public void Initialize(CProjectileDataSO data, CProjectilePool pool, Vector3 position, Vector2 direction)
    {
        _pool = pool;
        _direction = direction.normalized;
        _speed = data.Speed;
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

        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
        _remainingLifeTime -= Time.deltaTime;

        if (_remainingLifeTime <= 0.0f)
        {
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        if (_isReturned || _pool == null)
        {
            return;
        }

        _isReturned = true;
        _remainingLifeTime = 0.0f;
        _speed = 0.0f;
        _direction = Vector2.zero;
        _pool.ReturnToPool(this);
    }
}
