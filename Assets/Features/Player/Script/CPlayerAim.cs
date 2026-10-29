using UnityEngine;
using UnityEngine.InputSystem;

public class CPlayerAim : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private Transform _gunHolder;
    #endregion

    #region 내부 변수
    private Camera _mainCamera;
    #endregion

    private void Awake()
    {
        _mainCamera = Camera.main;

        if (_mainCamera == null || _gunHolder == null)
        {
            Debug.LogError("카메라 또는 GunHolder가 비어있음");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        Aim();
    }

    // 마우스 방향 조준
    private void Aim()
    {
        if (Mouse.current == null)
        {
            return;
        }
        
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        Vector2 gunPosition = new Vector2(_gunHolder.position.x, _gunHolder.position.y);

        Vector2 direction = mouseWorldPosition - gunPosition;

        if (direction == Vector2.zero)
        {
            return;
        }

        _gunHolder.right = direction;
    }
}