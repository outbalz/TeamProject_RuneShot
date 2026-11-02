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
     
    [SerializeField] private float _attackRange = 2f;
    private void Awake()
    {
        _enemy = GetComponent<CEnemy>(); 
        _navMove = GetComponent<NavMeshAgent>();

        
        _navMove.speed = _enemy.Enemydata.MovementSpeed; 
    }
    
    public void ActionEnter(Transform PlayerTr)
    {
        Debug.Log("Enter Move");

        _navMove.stoppingDistance = _attackRange;
    }
    public void ActionUpdate(Transform PlayerTr)
    {  
        if (PlayerTr == null)
        {
            return;
        }
         
        _navMove.destination = PlayerTr.position; 
    }
  
    public void ActionExit()
    {
        Debug.Log("Exit Move");
    }
     
  
}
