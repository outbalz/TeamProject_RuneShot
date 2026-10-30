using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour
{
     
    [SerializeField] private float _attackRange = 2f;

    private CEnemyMeleeAttack _enemy;

    private CEnemyAttack _attack;
    private CEnemyMove _move;
    private CEnemyIdle _idle;

    
    private void Awake()
    { 
        _enemy = GetComponent<CEnemyMeleeAttack>();

        _attack = GetComponent<CEnemyAttack>();
        _move = GetComponent<CEnemyMove>();
        _idle = GetComponent<CEnemyIdle>();
    }
    private void Update()
    {
         
       if (_enemy.PlayerTr != null && (_enemy.PlayerTr.transform.position - transform.position).magnitude <= _attackRange)
       { 
            _enemy.SetControll(_attack);  
       }
       else if (_enemy.PlayerTr != null)
       {
            _enemy.SetControll(_move);
       }
       else
       {
            _enemy.SetControll(_idle);
       }

        _enemy.Action();
    }
}
