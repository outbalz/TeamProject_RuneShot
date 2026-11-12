using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyRunaway : CEnemyActionBase
{
    #region 인스펙터
    [SerializeField]
    Animator _aniRunaway;
    [SerializeField]
    CEnemy _enemy;
    [SerializeField]
    NavMeshAgent _navMove;
    #endregion

    #region 내부변수
    string _Runaway = "IMove";
    #endregion
     
    private void Awake()
    {
        if (_aniRunaway == null)
        {
            _aniRunaway = GetComponent<Animator>();
        }

        if(_enemy == null)
        {
            _enemy = GetComponent<CEnemy>();
        }
        if(_navMove == null)
        {
            _navMove = GetComponent<NavMeshAgent>();
        }

    }
    public override void ActionEnter(Transform PlayerTr)
    {
        if(PlayerTr == null)
        {
            return;
        }

        _aniRunaway.SetInteger(_Runaway,1);
        _navMove.stoppingDistance = 0;
        Debug.Log("지금 호출되나?");
    }
    public override void ActionUpdate(Transform PlayerTr)
    {
        Vector3 dir = (transform.position - PlayerTr.position).normalized;
         
        _navMove.destination = transform.position + dir * _enemy.Enemydata.AttackRange;

        ReverseFlip(PlayerTr);
    }
    public override void ActionExit()
    {
        _navMove.stoppingDistance = 2;
        _aniRunaway.SetInteger(_Runaway, 0);
         
    }

    private void ReverseFlip(Transform PlayerTr)
    {
        if ((PlayerTr.position - transform.position).x > 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }
}
