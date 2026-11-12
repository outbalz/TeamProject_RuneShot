using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

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
    private CPlayerStateController _playerState;
    private CPlayerStatusHandler _playerStatus;
    private readonly Dictionary<CWeaponDataSO, int> _magazines = new Dictionary<CWeaponDataSO, int>();
    private IitemContainer _ammoSource;
    private float _reloadEndTime;
    #endregion

    #region 프로퍼티
    public bool IsAutomatic => _data != null && _data.Trigger != null && _data.Trigger.IsAutomatic;
    public bool IsReloading { get; private set; }

    public int MagazineAmmo
    {
        get
        {
            if (_data == null)
            {
                return 0;
            }

            if (_magazines.TryGetValue(_data, out int amount))
            {
                return amount;
            }

            return _data.MagazineSize;
        }
    }
    #endregion

    private void Awake()
    {
        _playerState = GetComponentInParent<CPlayerStateController>();
        _playerStatus = GetComponentInParent<CPlayerStatusHandler>();
    }

    private void Start()
    {
        if (HasInvalidReferences(_data))
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
        IsReloading = false;

        if (_playerInput != null)
        {
            _playerInput.enabled = false;
        }
    }

    private bool HasInvalidReferences(CWeaponDataSO data)
    {
        return data == null || data.Trigger == null || data.Projectile == null ||
            data.Projectile.Prefab == null || _projectilePool == null ||
            !_projectilePool.isActiveAndEnabled || _projectilePool.Prefab != data.Projectile.Prefab ||
            _muzzle == null;
    }

    public bool EquipWeapon(CWeaponDataSO data)
    {
        if (HasInvalidReferences(data))
        {
            Debug.LogError("무기 데이터, 투사체 풀 또는 Muzzle이 비어있음");
            return false;
        }

        if (_data == data)
        {
            return true;
        }

        IsReloading = false;
        _data = data;

        if (_playerInput != null)
        {
            _playerInput.ClearPendingShots();
        }

        return true;
    }

    public bool TryFire(Vector2 direction)
    {
        if (!isActiveAndEnabled || HasInvalidReferences(_data) ||
            direction == Vector2.zero || IsReloading || MagazineAmmo <= 0 || Time.time < _nextFireTime)
        {
            return false;
        }

        if (IsPlayerDead())
        {
            return false;
        }

        _magazines[_data] = MagazineAmmo - 1;
        _nextFireTime = Time.time + _data.FireInterval;
        _data.Trigger.TriggerWeapon(_data.Projectile, _projectilePool, _muzzle.position, direction).Forget();
        return true;
    }

    private bool IsPlayerDead()
    {
        return _playerState != null && _playerState.CurrentState == EPlayerState.Dead;
    }

    public void SetAmmoSource(IitemContainer ammoSource)
    {
        IsReloading = false;
        _ammoSource = ammoSource;
    }

    public bool TryReload()
    {
        if (!isActiveAndEnabled || HasInvalidReferences(_data) || IsPlayerDead() ||
            IsReloading || MagazineAmmo >= _data.MagazineSize || _data.AmmoItem == null ||
            _data.AmmoItem.ItemType != EItemtype.Ammunition ||
            !HasAmmoSource() || !HasAmmo(_data.AmmoItem))
        {
            return false;
        }

        float reloadTime = _data.ReloadTime;
        if (_playerStatus != null && _playerStatus.isActiveAndEnabled)
        {
            reloadTime *= _playerStatus.ReloadSpeed;
        }

        _reloadEndTime = Time.time + Mathf.Max(0.01f, reloadTime);
        IsReloading = true;

        if (_playerInput != null)
        {
            _playerInput.ClearPendingShots();
        }

        return true;
    }

    private void Update()
    {
        if (!IsReloading)
        {
            return;
        }

        if (IsPlayerDead() || HasInvalidReferences(_data) || !HasAmmoSource())
        {
            IsReloading = false;
            return;
        }

        if (Time.time < _reloadEndTime)
        {
            return;
        }

        CWeaponDataSO data = _data;
        int magazineAmmo = MagazineAmmo;
        int neededAmmo = data.MagazineSize - magazineAmmo;
        IsReloading = false;

        int loadedAmmo = TakeAmmo(data.AmmoItem, neededAmmo);
        _magazines[data] = magazineAmmo + loadedAmmo;
    }

    private bool HasAmmoSource()
    {
        return (UnityEngine.Object)_ammoSource != null;
    }

    private bool HasAmmo(CItemDataSO ammoItem)
    {
        IReadOnlyList<CItemSlotData> slots = _ammoSource.Inventory;
        for (int i = 0; i < slots.Count; i++)
        {
            CItemSlotData slot = slots[i];
            if (slot != null && slot.Item == ammoItem && slot.Amount > 0)
            {
                return true;
            }
        }

        return false;
    }

    private int TakeAmmo(CItemDataSO ammoItem, int neededAmmo)
    {
        IReadOnlyList<CItemSlotData> slots = _ammoSource.Inventory;
        int loadedAmmo = 0;
        for (int i = 0; i < slots.Count && loadedAmmo < neededAmmo; i++)
        {
            CItemSlotData slot = slots[i];
            if (slot == null || slot.Item != ammoItem || slot.Amount <= 0)
            {
                continue;
            }

            int before = slot.Amount;
            int amount = Mathf.Min(before, neededAmmo - loadedAmmo);
            _ammoSource.RemoveFromInventory(i, amount);
            CItemSlotData remaining = slots[i];
            int after = remaining != null && remaining.Item == ammoItem ? remaining.Amount : 0;
            loadedAmmo += Mathf.Clamp(before - after, 0, amount);
        }

        return loadedAmmo;
    }
}
