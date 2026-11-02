using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyIdle : MonoBehaviour, IEnemyControllInterface
{
    private NavMeshAgent _navMove;
    private Animator _aniIdle;

    private float _time;
    private float _timeInterval = 3f;
    private Coroutine _coroutine;

    private Vector3 _setPosition;

    private string _move = "IMove";
      
    private void Awake()
    {
        _navMove = GetComponent<NavMeshAgent>();
        _aniIdle = GetComponent<Animator>();

        _setPosition = transform.position;
    }
    public void ActionEnter(Transform PlayerTr)
    {
        Debug.Log("Enter Idle");

        _navMove.stoppingDistance = 0f; 

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(COReturnPosition());

    }
    public void ActionUpdate(Transform PlayerTr)
    { 
        _aniIdle.SetInteger(_move,0);
    }

    public void ActionExit()
    {
        Debug.Log("Exit Idle");

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    IEnumerator COReturnPosition()
    {
        while (_time < _timeInterval)
        {
            _time += Time.deltaTime;
            yield return null;
        }

        _navMove.velocity = Vector3.zero;
        _navMove.destination = _setPosition;
        _time = 0;

        if ((_setPosition - transform.position).x <= 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }
}
