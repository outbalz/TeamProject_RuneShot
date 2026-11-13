using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDisable : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _child;
    #endregion

    #region 내부 변수
    private bool _enabled;
    private float _time;
    [SerializeField] private float _interval = 0.3f;
    #endregion
     
    private void Awake()
    {
        if(_camera == null || _child == null)
        {
            Debug.Log("인스펙터 확인");
            _camera = Camera.main;
        }
 
    }
    private void Update()
    {
        DisableEnemy();
    }
    void DisableEnemy()
    {
        _time += Time.deltaTime;

        if(_time < _interval)
        {
            return;
        }

        _time = 0;

        Vector3 point = _camera.WorldToViewportPoint(transform.position);

        if (point.x < -0.2f || point.x > 1.2f
            || point.y < -0.2f || point.y > 1.2f)
        {
            _enabled = false;
        }
        else
        {
            _enabled = true; 
        }

        if(_child.activeSelf != _enabled)
        {
            _child.SetActive(_enabled);
        }
         
    }
}
