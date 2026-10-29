using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class CEnemyMeleeAttack : CEnemy
{


        
    EMonsterState _monsterState = EMonsterState.Idle;

    float _attackRange = 2f;

    Rigidbody2D _rb2D;

    float _speed = 2f;

    enum EMonsterState
    {
        Idle,
        Move,
        Attack,
    }
    protected override void Awake()
    {
        base.Awake();
        
        _rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
        if (PlayerTr != null && (PlayerTr.transform.position - transform.position).magnitude <= _attackRange)
        {
            _monsterState = EMonsterState.Attack;
        }
        else if(PlayerTr != null)
        {
            _monsterState = EMonsterState.Move;
        }
        else
        {
            _monsterState = EMonsterState.Idle;
        }
         
        
    }
    private void FixedUpdate()
    {
        Action();
    }

    void Action()
    {
        switch (_monsterState)
        {
            case EMonsterState.Idle:
                Idle();
                break;
            case EMonsterState.Move:
                Move();
                break;
            case EMonsterState.Attack:
                Attack();
                break;
            default:
                break;
        }
    }
    void Idle()
    {
        Debug.Log("Idle");
    }
    void Attack()
    {
        Debug.Log("Attack");
       
        
    }

    private void Move()
    {
        Debug.Log("Move");


        Vector3 dir = Vector3.Normalize(PlayerTr.position - transform.position);

        _rb2D.velocity = dir* _speed * Time.fixedDeltaTime;
        
        
    }

    private void Rotation()
    {
        
    }
}
