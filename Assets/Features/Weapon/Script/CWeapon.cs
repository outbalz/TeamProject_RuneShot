using Cysharp.Threading.Tasks;
using UnityEngine;

public class CWeapon : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private CWeaponDataSO _data;
    [SerializeField] private CProjectilePool _projectilePool;
    [SerializeField] private Transform _gunHolder;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private string _triggerActionName = "TriggerWeapon";
    [SerializeField] private bool _usePlayerInput = true;
    #endregion

    #region 내부 변수
    private CPlayerWeaponInput _playerInput;
    private float _nextFireTime;
    #endregion

    #region 프로퍼티
    public bool IsAutomatic => _data != null && _data.Trigger != null && _data.Trigger.IsAutomatic;
    #endregion

    private void Start()
    {
        if (HasInvalidReferences())
        {
            Debug.LogError("무기 데이터, 투사체 풀 또는 Muzzle이 비어있음");
            enabled = false;
            return;
        }

        if (_usePlayerInput)
        {
            if (_gunHolder == null)
            {
                Debug.LogError("GunHolder가 비어있음");
                return;
            }

            _playerInput = GetComponent<CPlayerWeaponInput>();

            if (_playerInput == null)
            {
                _playerInput = gameObject.AddComponent<CPlayerWeaponInput>();
            }

            _playerInput.Initialize(this, _gunHolder, _triggerActionName);
        }
    }

    private void OnEnable()
    {
        if (_playerInput != null)
        {
            _playerInput.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (_playerInput != null)
        {
            _playerInput.enabled = false;
        }
    }

    private bool HasInvalidReferences()
    {
        return _data == null || _data.Trigger == null || _data.Projectile == null ||
            _data.Projectile.Prefab == null || _projectilePool == null ||
            !_projectilePool.isActiveAndEnabled || _projectilePool.Prefab != _data.Projectile.Prefab ||
            _muzzle == null;
    }

    public bool TryFire(Vector2 direction)
    {
        if (!isActiveAndEnabled || HasInvalidReferences() ||
            direction == Vector2.zero || Time.time < _nextFireTime)
        {
            return false;
        }

        _nextFireTime = Time.time + _data.FireInterval;
        _data.Trigger.TriggerWeapon(_data.Projectile, _projectilePool, _muzzle.position, direction).Forget();
        return true;
    }
}
