using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyIdle : MonoBehaviour, IEnemyControllInterface
{
    private NavMeshAgent _navMove;

    private float _time;
    private float _timeInterval = 3f;
    private Coroutine _coroutine;

    private Vector3 _setPosition;

    private void Awake()
    {
        _navMove = GetComponent<NavMeshAgent>();

        _setPosition = transform.position;
    }
    public void ActionEnter(Transform PlayerTr)
    {
        _navMove.stoppingDistance = 0f;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(COReturnPosition());

    }
    public void ActionUpdate(Transform PlayerTr)
    {
        Debug.Log("Idle"); 
    }

    public void ActionExit()
    {
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

        _navMove.destination = _setPosition;
        _time = 0;
    }
}
