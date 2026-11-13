using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour
{
    #region 인스펙터 
    [SerializeField] private CEnemy _enemy;
    [SerializeField] private CEnemyFSM _enemyFSM;
    [SerializeField] private CEnemyAttack _attack;
    [SerializeField] private CEnemyMove _move;
    [SerializeField] private CEnemyIdle _idle; 
    [SerializeField] private CEnemyReturn _return;
    #endregion

    #region 내부변수 
    private Vector3 _setPosition;
    private float _attackRange;
    #endregion
    
    private void Awake()
    { 
        if (_enemyFSM == null || _enemy == null || _attack == null || _move == null || _idle == null || _return == null)
        {
            Debug.Log("인스펙터 확인");
        } 
       _setPosition = transform.position;
        _attackRange = _enemy.Enemydata.AttackRange;
    }
    private void Update()
    {
        if (!_enemy.IsAlive)
        {
            return;
        }
     
        if(_enemy.PlayerTr == null)
        {
            float distance = (_setPosition - transform.position).sqrMagnitude;

            if (distance < 0.1f)
            {
                _enemyFSM.SetControll(_idle);
            }
            else
            {
                _enemyFSM.SetControll(_return);
            }
        }
        else
        {
            float distance = (_enemy.PlayerTr.transform.position - transform.position).sqrMagnitude;

            if (distance <= _attackRange* _attackRange)
            {
                _enemyFSM.SetControll(_attack);
            }
            else
            {
                _enemyFSM.SetControll(_move);
            }
        }

            _enemyFSM.Action();

    }
}
