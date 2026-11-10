using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyAttackState : CEnemyBaseState
{
    Transform transform;
    public CEnemyAttackState(CEnemyControllerTest cEnemyController) : base(cEnemyController)
    {
        if (_enemy == null)
        {
             _enemy =  cEnemyController.GetComponent<CEnemy>();
        }
        if (_aniAttack == null)
        {
            _aniAttack = cEnemyController.GetComponent<Animator>();
        }
        transform = cEnemyController.transform;
        _attackInterval = _enemy.Enemydata.WeaponFireInterval;
        _damage = _enemy.Enemydata.WeaponDamage;
    }
    #region 인스펙터
    [SerializeField] protected CEnemy _enemy;
    [SerializeField] protected Animator _aniAttack;
    #endregion

    #region 내부변수
    protected float _attackInterval;
    protected float _time;
    protected string _attack = "TAttack";
    protected CPlayerHPHandler _playerHP;
    protected float _damage;
    #endregion

    private void Awake()
    {
       //if (_enemy == null)
       //{
       //    _enemy = GetComponent<CEnemy>();
       //}
       //if (_aniAttack == null)
       //{
       //    _aniAttack = GetComponent<Animator>();
       //}

        _attackInterval = _enemy.Enemydata.WeaponFireInterval;
        _damage = _enemy.Enemydata.WeaponDamage;
    }

    public void ActionEnter(Transform PlayerTr)
    {
        Debug.Log("Enter Attack");
        if (_playerHP == null)
        {
            _playerHP = PlayerTr.GetComponent<CPlayerHPHandler>();
        }
    }
    public virtual void ActionUpdate(Transform PlayerTr)
    {
        if (PlayerTr == null || _playerHP == null)
        {
            return;
        }

        Flip(PlayerTr);

        _time += Time.deltaTime;

        if (_time < _attackInterval)
        {
            return;
        }

        _playerHP.OnHit(_damage);

        _aniAttack.SetTrigger(_attack);
        _time = 0;

    }

    public void ActionExit()
    {
        Debug.Log("Exit Attack");
        _time = 0;
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
