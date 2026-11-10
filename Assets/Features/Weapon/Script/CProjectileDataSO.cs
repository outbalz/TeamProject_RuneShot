using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class CProjectileDataSO : CGameDataSO
{
    #region 인스펙터
    [SerializeField] private CProjectile _prefab;
    [SerializeField, Min(0.01f)] private float _speed = 10.0f;
    [SerializeField, Min(0.01f)] private float _lifeTime = 2.0f;
    [SerializeField, Min(0.0f)] private float _damage = 10.0f;
    #endregion

    #region 프로퍼티
    public CProjectile Prefab => _prefab;
    public float Speed => _speed;
    public float LifeTime => _lifeTime;
    public float Damage => _damage;
    #endregion

    //투사체가 벽이나 오브젝트에 충돌했을 경우 호출
    public abstract UniTask OnImpact();

    //투사체가 케릭터에게 맞았을 때 호출
    public abstract UniTask OnHit();
}
