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

    private Vector3 _setPosition;

    private float _time;
    private float _timeInterval = 3f;
    private Coroutine _coroutine;

    private void Awake()
    {
        _enemy = GetComponent<CEnemy>(); 
        _navMove = GetComponent<NavMeshAgent>();

        _setPosition = transform.position; 
        _navMove.speed = _enemy.Enemydata.MovementSpeed; 
    }
    
    public void ActionEnter(Transform PlayerTr)
    {
        ActionUpdate(PlayerTr);
    }
    public void ActionUpdate(Transform PlayerTr)
    {
        Debug.Log("Move");

        if (PlayerTr == null)
        {
            return;
        }
         
        _navMove.destination = PlayerTr.position;  
    }
  
    public void ActionExit()
    {  
        if(_coroutine != null )
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(COReturnPosition()); 
    }

    IEnumerator COReturnPosition()
    {
        while(_time < _timeInterval)
        {
            _time += Time.deltaTime;
            yield return null;
        }
        
        _navMove.destination = _setPosition;
        _time = 0;
    }
  
}
