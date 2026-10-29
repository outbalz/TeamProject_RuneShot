using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemy : MonoBehaviour
{

    [SerializeField]
    private CEnemyDetect _detectObject;

     
    private Transform _playerTr; 
    public Transform PlayerTr { get { return _playerTr; } set { _playerTr = value; } }

    protected virtual void Awake()
    {
        Debug.Log("디텍트 오브젝트 생성");
        Instantiate(_detectObject, transform);
    }

    protected void Die()
    {

    }
}
