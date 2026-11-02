using Cysharp.Threading.Tasks;
using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI; 

public class CEnemyMove : MonoBehaviour,IEnemyControllInterface
{
     
    private CEnemy _enemy; 
    private NavMeshAgent _navMove;
    private Animator _aniMove;
    private string _move = "IMove";

    [SerializeField] private float _attackRange = 2f;
    private void Awake()
    {
        _enemy = GetComponent<CEnemy>(); 
        _navMove = GetComponent<NavMeshAgent>();
        _aniMove = GetComponent<Animator>();

        _navMove.speed = _enemy.Enemydata.MovementSpeed; 
    }
    
    public void ActionEnter(Transform PlayerTr)
    {
        Debug.Log("Enter Move");

        _navMove.stoppingDistance = _attackRange;

        _aniMove.SetInteger(_move, 1);
    }
    public void ActionUpdate(Transform PlayerTr)
    { 
        if (PlayerTr == null)
        {
            return;
        }
         
        _navMove.destination = PlayerTr.position;
        

        if ((PlayerTr.position - transform.position).x <= 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

    }
  
    public void ActionExit()
    {
        _aniMove.SetInteger(_move, 0);
        Debug.Log("Exit Move");
    }

    
}
