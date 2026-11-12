using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDisable : MonoBehaviour
{ 
     
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _child;

    private bool _enabled;

    private void Update()
    {
        DisableEnemy();
    }
    void DisableEnemy()
    {
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
