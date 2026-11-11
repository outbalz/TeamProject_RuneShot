using UnityEngine;

public class CPlayerWeaponInput : MonoBehaviour
{
    #region 내부 변수
    private CWeapon _weapon;
    private Transform _gunHolder;
    private CInputActionData _triggerInput;
    private int _pendingShots;
    #endregion

    public void Initialize(CWeapon weapon, Transform gunHolder, string actionName)
    {
        if (_triggerInput != null)
        {
            _triggerInput.OnPerformedAction -= InputTrigger;
            _triggerInput = null;
        }

        _weapon = weapon;
        _gunHolder = gunHolder;
        _pendingShots = 0;
        CInputDispatcher dispatcher = CInputDispatcher.Instance;
        CInputActionData input;

        if (dispatcher == null || !dispatcher.InputActionMap.TryGetValue(actionName, out input) ||
            input.Reference == null || input.Reference.action == null)
        {
            Debug.LogError("TriggerWeapon 입력 참조가 비어있음");
            enabled = false;
            return;
        }

        _triggerInput = input;

        if (isActiveAndEnabled)
        {
            _triggerInput.OnPerformedAction += InputTrigger;
        }
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

    public void ClearPendingShots()
    {
        _pendingShots = 0;
    }

    private void InputTrigger()
    {
        _pendingShots++;
    }

    private void LateUpdate()
    {
        if (_weapon == null || !_weapon.isActiveAndEnabled || _gunHolder == null || _triggerInput == null)
        {
            _pendingShots = 0;
            return;
        }

        if (_weapon.IsAutomatic)
        {
            if (_triggerInput.Reference.action.IsPressed())
            {
                _weapon.TryFire(_gunHolder.right);
            }
        }
        else
        {
            for (int i = 0; i < _pendingShots; i++)
            {
                _weapon.TryFire(_gunHolder.right);
            }
        }

        _pendingShots = 0;
    }
}
