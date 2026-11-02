using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemy : MonoBehaviour
{

    [SerializeField]
    private CEnemyDetect _detectObject;

    [SerializeField]
    private CEnemyStatusDataSO _enemydata;

    public CEnemyStatusDataSO Enemydata => _enemydata;

    private Transform _playerTr; 
    public Transform PlayerTr { get { return _playerTr; } set { _playerTr = value; } }

    protected bool _isAlive = true;
      
    protected float _hp;

    protected virtual void Awake()
    { 
        Debug.Log("디텍트 오브젝트 생성");
        //Instantiate(_detectObject, transform.position + transform.up * _enemydata.DetectionDistance, transform.rotation,transform); 
        Instantiate(_detectObject,transform);
        _hp = _enemydata.MaxHP;
    }

    public bool Die()
    {
        if (_hp <= 0)
        {
            _hp = 0;

            _isAlive = false;
        }    
        

        return _isAlive;
    }
}
