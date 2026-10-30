using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour
{
     
    [SerializeField] private float _attackRange = 2f;

    [SerializeField] private CEnemyFSM _enemyFSM;

    [SerializeField] private CEnemyAttack _attack;
    [SerializeField] private CEnemyMove _move;
    [SerializeField] private CEnemyIdle _idle;

    
    private void Awake()
    { 
        if(_enemyFSM == null)
        {
            _enemyFSM = GetComponent<CEnemyFSM>();
        }
        
        if( _attack == null)
        {
            _attack = GetComponent<CEnemyAttack>();
        }
        if(_move == null)
        {
            _move = GetComponent<CEnemyMove>();
        }
        if(_idle == null)
        {
            _idle = GetComponent<CEnemyIdle>();
        }
          
    }
    private void Update()
    {
         
       if (_enemyFSM.PlayerTr != null && (_enemyFSM.PlayerTr.transform.position - transform.position).magnitude <= _attackRange)
       {
            _enemyFSM.SetControll(_attack);  
       }
       else if (_enemyFSM.PlayerTr != null)
       {
            _enemyFSM.SetControll(_move);
       }
       else
       {
            _enemyFSM.SetControll(_idle);
       }

        _enemyFSM.Action();
    }
}
