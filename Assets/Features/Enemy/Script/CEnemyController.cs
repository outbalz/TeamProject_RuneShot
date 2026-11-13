using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour
{
    #region 인스펙터 
    [SerializeField] protected CEnemy _enemy;
    [SerializeField] protected CEnemyFSM _enemyFSM;
    [SerializeField] protected CEnemyAttack _attack;
    [SerializeField] protected CEnemyMove _move;
    [SerializeField] protected CEnemyIdle _idle; 
    [SerializeField] protected CEnemyReturn _return; 
    #endregion

    #region 내부변수 
    protected Vector3 _setPosition;
    protected float distance = 0;
    #endregion
   
    protected virtual void Awake()
    { 
        if (_enemyFSM == null || _enemy == null || _attack == null || _move == null || _idle == null || _return == null)
        {
            Debug.Log("인스펙터 확인");
        } 
       _setPosition = transform.position; 
    }
    protected virtual void Update()
    {
        if (!_enemy.IsAlive)
        {
            return;
        }

        if (_enemy.PlayerTr != null)
        {
            distance = (_enemy.PlayerTr.transform.position - transform.position).magnitude;
        }

        if (_enemy.PlayerTr == null && (_setPosition - transform.position).sqrMagnitude < 0.1f)
        {
            _enemyFSM.SetControll(_idle);
        }
        else if (_enemy.PlayerTr == null)
        {
            _enemyFSM.SetControll(_return);
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
