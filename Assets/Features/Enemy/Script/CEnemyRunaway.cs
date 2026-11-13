using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using static CEnemyAttackArcher;
public class CEnemyRunaway
{
    #region 인스펙터
    [SerializeField]
    Animator _aniRunaway;
    [SerializeField]
    CEnemy _enemy;
    [SerializeField]
    NavMeshAgent _navMove;
    Transform _transform;
    Bool _isActing;
    #endregion

    #region 내부변수
    string _Runaway = "IMove";
    #endregion
     
    public CEnemyRunaway(Animator aniRunaway, CEnemy enemy, NavMeshAgent navMove, Transform transform, Bool isActing)
    {
        _aniRunaway = aniRunaway;
        _enemy = enemy;
        _navMove = navMove;
        _transform = transform;
        _isActing = isActing;
    }
    //private void Awake()
    //{
    //    if (_aniRunaway == null || _enemy == null || _navMove == null)
    //    {
    //        Debug.Log("인스펙터 확인");
    //    } 
    //}
    //public override void ActionEnter(Transform PlayerTr)
    //{
    //    if(PlayerTr == null)
    //    {
    //        return;
    //    }
    //
    //    _aniRunaway.SetInteger(_Runaway,1);
    //    _navMove.stoppingDistance = 0;
    //    Debug.Log("지금 호출되나?");
    //}
    //public override void ActionUpdate(Transform PlayerTr)
    //{
    //    Vector3 dir = (transform.position - PlayerTr.position).normalized;
    //     
    //    _navMove.destination = transform.position + dir * _enemy.Enemydata.AttackRange;
    //
    //    ReverseFlip(PlayerTr);
    //}
    //public override void ActionExit()
    //{
    //    _navMove.stoppingDistance = 2;
    //    _aniRunaway.SetInteger(_Runaway, 0);
    //     
    //}
    //
    private void ReverseFlip(Transform PlayerTr)
    {
        if ((PlayerTr.position - _transform.position).x > 0)
        {
            _transform.rotation = Quaternion.identity;
        }
        else
        {
            _transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }
    private bool UpdateRun(Transform PlayerTr,float distance)
    { 
        Vector3 dir = (_transform.position - PlayerTr.position).normalized;

        _navMove.destination = _transform.position + dir * _enemy.Enemydata.AttackRange;

        ReverseFlip(PlayerTr);

        if((PlayerTr.position - _transform.position).sqrMagnitude >= distance * distance)
        {
            return true;
        }
        Debug.Log("호출?");
        return false;
    }
    
    public async UniTask Runaway(Transform PlayerTr, float distance,CancellationTokenSource source)
    {
        if (PlayerTr == null)
        {
            return;
        }

        source = new CancellationTokenSource();

        _aniRunaway.SetInteger(_Runaway, 1);
        _navMove.stoppingDistance = 0;

        try
        {
            await UniTask.WaitUntil(() => UpdateRun(PlayerTr, distance), PlayerLoopTiming.Update, source.Token);
        }
        finally
        {
            _isActing._isActing = false;
            _navMove.stoppingDistance = 2;
            _aniRunaway.SetInteger(_Runaway, 0);

            source.Dispose();
            source = null;
        }
    }
}
