using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;

public class CEnemyAttackBoss : CEnemyAttack
{
    #region 인스펙터
    [SerializeField] private NavMeshAgent _navMove;
    [SerializeField] private SpriteRenderer _meleeAttack;
    [SerializeField] private SpriteRenderer _rangeAttack;
    #endregion

    #region 내부변수
    private string _attackMelee = "TAttack";
    private string _attackRange = "TAttackRange";
    private string _attackCharge = "IMove";
    private bool _isCharging;
    private float _chargeAttackSpeed = 10f;
    private CancellationTokenSource _source;
    #endregion

    private void Awake()
    {
        if (_enemy == null || _aniAttack == null || _navMove == null)
        {
            Debug.Log("인스펙터 확인"); 
        }
        
        _attackInterval = _enemy.Enemydata.WeaponFireInterval;
        _damage = _enemy.Enemydata.WeaponDamage; 
    }

    public override void ActionUpdate(Transform PlayerTr)
    {
        if (PlayerTr == null || _playerHP == null)
        {
            return;
        }

        if (_isCharging)
        {
            return;
        }

        Flip(PlayerTr);

        _time += Time.deltaTime;

        if (_time < _attackInterval)
        {
            return;
        }

        float distance = (PlayerTr.position - transform.position).magnitude;

        if (distance < 2f)
        { 
            ChangeAlpha(true);
            _playerHP.OnHit(_damage);
            _aniAttack.SetTrigger(_attackMelee);
        }
        else if (distance < 6f)
        {
            ChangeAlpha(false);
            _aniAttack.SetTrigger(_attackRange);
        }

        else 
        {
            _source = new CancellationTokenSource();
            ChargeAttack(PlayerTr,_source).Forget();
        }
         
        _time = 0;

    }
     
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isCharging)
        {

            if (collision.collider.tag == "Player")
            {
                _playerHP.OnHit(_damage);
                IsCharging(false);
            }
        }
    } 


    private void ChangeAlpha(bool isMeleeAttack)
    {
        Color colorMelee = _meleeAttack.color;
        Color colorRange = _rangeAttack.color;

        if (isMeleeAttack)
        {
            colorMelee.a = 1f;
            colorRange.a = 0f;
        }
        else
        {
            colorMelee.a = 0f;
            colorRange.a = 1f;
        }

        _meleeAttack.color = colorMelee;
        _rangeAttack.color = colorRange;
    }

    private async UniTask ChargeAttack(Transform PlayerTr, CancellationTokenSource source)
    {

        IsCharging(true);

        if (!_navMove.pathPending)
        {
            _navMove.SetDestination(PlayerTr.position);
        }

        await UniTask.WaitUntil(() => ArriveDestination(PlayerTr), PlayerLoopTiming.Update, cancellationToken: destroyCancellationToken,source.IsCancellationRequested);

        IsCharging(false);
    }

    private void IsCharging(bool ischarging)
    {
        _isCharging = ischarging;

        if (_isCharging)
        {
            _navMove.speed = _chargeAttackSpeed;
            _navMove.stoppingDistance = 0;
            _aniAttack.SetInteger(_attackCharge, 1);
        }
        else
        {
            _navMove.speed = _enemy.Enemydata.MovementSpeed;
            _navMove.stoppingDistance = _enemy.Enemydata.AttackRange;
            _aniAttack.SetInteger(_attackCharge, 0);
        }

    } 
    private bool ArriveDestination(Transform PlayerTr)
    {
        Debug.Log("호출중?");

        if (_navMove.pathStatus == NavMeshPathStatus.PathInvalid || _navMove.pathStatus == NavMeshPathStatus.PathPartial)
        {
            _navMove.SetDestination(_navMove.pathEndPosition);
        }

        if ((_navMove.destination - transform.position).magnitude < 1f)
        {
            return true;
        }

        return false;
    }

    private void Flip(Transform PlayerTr)
    {
        if ((PlayerTr.position - transform.position).x <= 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }
   
}
