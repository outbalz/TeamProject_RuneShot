using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyControllerSwardman : CEnemyController
{
   

    protected override void Awake()
    {
        base.Awake();  
    }
    protected override void Update()
    {
       
        base.Update();

        if (_enemy.PlayerTr != null && (_enemy.PlayerTr.transform.position - transform.position).magnitude <= _enemy.Enemydata.AttackRange)
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
