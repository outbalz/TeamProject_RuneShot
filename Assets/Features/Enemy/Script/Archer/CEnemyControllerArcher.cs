using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyControllerArcher : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private CEnemy _enemy;
    [SerializeField] private CEnemyFSM _enemyFSM;
    [SerializeField] private CEnemyAttack _attack;
    [SerializeField] private CEnemyMove _move;
    [SerializeField] private CEnemyIdle _idle;
    [SerializeField] private CEnemyDeath _death;
    [SerializeField] private CEnemyReturn _return;
    [SerializeField] private CEnemyRunaway _runaway;
    #endregion

    #region 내부변수 
    Vector3 _setPosition;
    #endregion

    private void Awake()
    {
        #region Getcomponent
        if (_enemyFSM == null)
        {
            _enemyFSM = GetComponent<CEnemyFSM>();
        }
        if(_enemy == null)
        {
            _enemy = GetComponent<CEnemy>();
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

        if(_death == null)
        {
            _death = GetComponent<CEnemyDeath>();
        }
        if(_return == null)
        {
            _return = GetComponent<CEnemyReturn>();
        }
        if(_runaway == null)
        {
            _runaway = GetComponent<CEnemyRunaway>();
        }
        #endregion

        _setPosition = transform.position;
    }
    private void Update()
    {
        if(!_enemy.IsAlive)
        {
            return;
        }

        if (_enemy.PlayerTr != null && (_enemy.PlayerTr.transform.position - transform.position).magnitude <= 2f)
        {
            _enemyFSM.SetControll(_runaway);
        }
        else if (_enemy.PlayerTr != null && (_enemy.PlayerTr.transform.position - transform.position).magnitude <= _enemy.Enemydata.AttackRange)
        {
            _enemyFSM.SetControll(_attack);
        } 
        else if (_enemy.PlayerTr != null)
        {
            _enemyFSM.SetControll(_move);
        }
        else if (_enemy.PlayerTr == null && (_setPosition - transform.position).sqrMagnitude < 0.1f)
        {
            _enemyFSM.SetControll(_idle);
        }
        else if (_enemy.PlayerTr == null)
        {
            _enemyFSM.SetControll(_return);
        }
         
       //if (!_enemy.Die())
       //{
       //    _enemyFSM.SetControll(_death);
       //}

        _enemyFSM.Action();
    }
}
