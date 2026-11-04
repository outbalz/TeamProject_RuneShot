using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyControllerArcher : CEnemyController
{
    #region 인스펙터
    [SerializeField] private CEnemyRunaway _runaway;
    #endregion

    #region 내부변수
    [SerializeField] private float _runawayDistance = 2f;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        if(_runaway == null)
        {
            _runaway = GetComponent<CEnemyRunaway>();
        }
    }
    protected override void Update()
    {
         
        base.Update();

        if (_enemy.PlayerTr != null && (_enemy.PlayerTr.transform.position - transform.position).magnitude <= _runawayDistance)
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
        //if (!_enemy.Die())
        //{
        //    _enemyFSM.SetControll(_death);
        //}

        _enemyFSM.Action();

    }
}
