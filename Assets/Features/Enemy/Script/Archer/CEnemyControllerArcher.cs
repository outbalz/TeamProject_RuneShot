using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyControllerArcher : CEnemyController
{
    #region 인스펙터
    //[SerializeField] private CEnemyRunaway _runaway;
    #endregion

    #region 내부변수
    [SerializeField] private float _runawayDistance = 2f;
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
       //if(_runaway == null)
       //{
       //    Debug.Log("인스펙터 확인");
       //}
    }
    protected override void Update()
    {
        if (!_enemy.IsAlive)
        {
            return;
        }

        base.Update();
           
        if (_enemy.PlayerTr != null && distance <= _runawayDistance)
        {
            //_enemyFSM.SetControll(_runaway);
        }
        else if (_enemy.PlayerTr != null && distance <= _enemy.Enemydata.AttackRange)
        {
            _enemyFSM.SetControll(_attack);
        }
        else if (_enemy.PlayerTr != null)
        {
            _enemyFSM.SetControll(_move);
        }
          
        _enemyFSM.Action();

    }
}
