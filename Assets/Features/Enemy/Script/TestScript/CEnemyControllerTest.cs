using System.Collections.Generic;
using UnityEngine;

public class CEnemyControllerTest : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] List<EnemyBaseSO> stateSOList;
    [SerializeField] List<CEnemyBaseState> stateList;

    [SerializeField] protected CEnemy _enemy;
    [SerializeField] protected CEnemyFSM _enemyFSM;
    [SerializeField] protected CEnemyAttack _attack;
    [SerializeField] protected CEnemyMove _move;
    [SerializeField] protected CEnemyIdle _idle;
    [SerializeField] protected CEnemyDeath _death;
    [SerializeField] protected CEnemyReturn _return;
    #endregion

    #region 내부변수 
    protected Vector3 _setPosition;
    protected float distance = 0;
    #endregion

    void Inistate()
    {
        foreach (EnemyBaseSO enemy in stateSOList)
        {
            stateList.Add(enemy.createState(this));
        }
    }

    protected virtual void Awake()
    {
        #region Getcomponent
        if (_enemyFSM == null)
        {
            _enemyFSM = GetComponent<CEnemyFSM>();
        }
        if (_enemy == null)
        {
            _enemy = GetComponent<CEnemy>();
        }

        if (_attack == null)
        {
            _attack = GetComponent<CEnemyAttack>();
        }
        if (_move == null)
        {
            _move = GetComponent<CEnemyMove>();
        }
        if (_idle == null)
        {
            _idle = GetComponent<CEnemyIdle>();
        }

        if (_death == null)
        {
            _death = GetComponent<CEnemyDeath>();
        }
        if (_return == null)
        {
            _return = GetComponent<CEnemyReturn>();
        }
        #endregion

        _setPosition = transform.position;

        stateList[0].ActionEnter();

    }
    protected virtual void Update()
    {

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

        //if(!_enemy.Die())
        //{
        //    _enemyFSM.SetControll(_death);
        //}


    }
}