using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyReturn : MonoBehaviour, IEnemyControllInterface
{
    private NavMeshAgent _navMove;
    private Animator _aniIdle;

    private float _time;
    private float _timeInterval = 3f;
    private Coroutine _coroutine;

    private Vector3 _setPosition;

    private string _move = "IMove";

    CancellationTokenSource _tokenSource;
    private void Awake()
    {
        _navMove = GetComponent<NavMeshAgent>();
        _aniIdle = GetComponent<Animator>();

        _setPosition = transform.position;
    }
     
    private void OnDestroy()
    {
        if( _coroutine != null )
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        } 
    }
     
    public void ActionEnter(Transform PlayerTr)
    {   
        if(_tokenSource == null)
        {
            _tokenSource = new CancellationTokenSource();
        }
        else
        {
            return;
        }    

        _navMove.stoppingDistance = 0f;

        ReturnPosition(_tokenSource).Forget();
    }
    public void ActionUpdate(Transform PlayerTr)
    {

    }
    public void ActionExit()
    { 
        if (_coroutine != null)
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        } 
    }

    async UniTask ReturnPosition(CancellationTokenSource tokenSource)
    { 
        _aniIdle.SetInteger(_move, 1);

        await UniTask.Delay(TimeSpan.FromSeconds(3f),false,PlayerLoopTiming.Update,tokenSource.Token);

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
         
        _tokenSource.Dispose();
        _tokenSource = null; 
    }

    
}
