using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyReturn : MonoBehaviour, IEnemyControllInterface
{
    #region 인스펙터
    [SerializeField] private NavMeshAgent _navMove;
    [SerializeField] private Animator _aniReturn;
    #endregion

    #region 내부변수
    private float _timeInterval = 3f;
    private Coroutine _coroutine;

    private Vector3 _setPosition;

    private string _move = "IMove";

    CancellationTokenSource _tokenSource;
    #endregion

    private void Awake()
    {
        if(_navMove == null)
        {
            _navMove = GetComponent<NavMeshAgent>();
        }
        if( _aniReturn == null)
        {
            _aniReturn = GetComponent<Animator>();
        }  

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
        _aniReturn.SetInteger(_move, 1);

        await UniTask.Delay(TimeSpan.FromSeconds(_timeInterval),false,PlayerLoopTiming.Update,tokenSource.Token);

        _navMove.velocity = Vector3.zero;
        _navMove.destination = _setPosition; 

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
