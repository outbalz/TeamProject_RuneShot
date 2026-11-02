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
    [SerializeField] private CEnemyDamaged _damaged;
    [SerializeField] private CEnemyDeath _death;
    [SerializeField] private CEnemyReturn _return;
    
    Vector3 _setPosition;
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

        if(_damaged == null)
        {
            _damaged = GetComponent<CEnemyDamaged>();
        }
        if(_death == null)
        {
            _death = GetComponent<CEnemyDeath>();
        }
        if(_return == null)
        {
            _return = GetComponent<CEnemyReturn>();
        }

        _setPosition = transform.position;
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
       else if(_enemyFSM.PlayerTr == null && (_setPosition - transform.position).magnitude < 0.1f)
       { 
            _enemyFSM.SetControll(_idle);
       }
       else if(_enemyFSM.PlayerTr == null)
       {
            _enemyFSM.SetControll(_return);
       }

       if(!_enemyFSM.Die())
       {
           _enemyFSM.SetControll(_death);
       }

        _enemyFSM.Action();
    }
}
