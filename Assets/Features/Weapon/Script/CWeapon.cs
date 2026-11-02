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
    #endregion

    #region 내부 변수
    private CInputActionData _triggerInput;
    private int _pendingShots;
    #endregion

    private void Start()
    {
        if (_data == null || _data.Trigger == null || _data.Projectile == null ||
            _data.Projectile.Prefab == null || _projectilePool == null ||
            !_projectilePool.isActiveAndEnabled || _projectilePool.Prefab != _data.Projectile.Prefab ||
            _gunHolder == null || _muzzle == null)
        {
            Debug.LogError("무기 데이터, 투사체 풀, GunHolder 또는 Muzzle이 비어있음.");
            enabled = false;
            return;
        }

        CInputDispatcher dispatcher = CInputDispatcher.Instance;

        if (dispatcher == null ||
            !dispatcher.InputActionMap.TryGetValue(_triggerActionName, out _triggerInput))
        {
            Debug.LogError("TriggerWeapon Action이 없음");
            enabled = false;
            return;
        }

        _triggerInput.OnPerformedAction += InputTrigger;
    }

    private void OnEnable()
    {
        if (_triggerInput != null)
        {
            _triggerInput.OnPerformedAction += InputTrigger;
        }
    }

    private void OnDisable()
    {
        if (_triggerInput != null)
        {
            _triggerInput.OnPerformedAction -= InputTrigger;
        }

        _pendingShots = 0;
    }

    private void InputTrigger()
    {
        _pendingShots++;
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _pendingShots; i++)
        {
            _data.Trigger.TriggerWeapon(_data.Projectile, _projectilePool, _muzzle.position, _gunHolder.right).Forget();
        }

        _pendingShots = 0;
    }
}
