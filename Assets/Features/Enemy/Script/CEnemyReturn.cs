using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyReturn : CEnemyActionBase
{
    #region 인스펙터
    [SerializeField] private NavMeshAgent _navMove;
    [SerializeField] private Animator _aniReturn;
    [SerializeField] private Collider2D _col;
    #endregion

    #region 내부변수
    private float _maxReturnTime = 3f;
    private float _time;

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
        if(_col == null)
        {
            _col = GetComponent<BoxCollider2D>();
        }
        _setPosition = transform.position;
    }
     
    private void OnDestroy()
    {
        if(_tokenSource != null )
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        } 
    }
     
    public override void ActionEnter(Transform PlayerTr)
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
 
    public override void ActionExit()
    { 
        if (_tokenSource != null)
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        } 
    }
    async UniTask ReturnPosition(CancellationTokenSource tokenSource)
    {
        _aniReturn.SetInteger(_move, 1);
          
        await UniTask.WaitUntil(IsDestinationArrive, PlayerLoopTiming.Update, tokenSource.Token);

        float _time = UnityEngine.Random.Range(0f, _maxReturnTime);

        await UniTask.Delay(TimeSpan.FromSeconds(_maxReturnTime), false, PlayerLoopTiming.Update, tokenSource.Token);

        _navMove.velocity = Vector3.zero;
        _navMove.destination = _setPosition;
        _aniReturn.SetInteger(_move, 1);

        Flip();

        _tokenSource.Dispose();
        _tokenSource = null;
    }

    private bool IsDestinationArrive()
    {
        bool isArrive = false;
         
        
        if ((_navMove.destination - transform.position).magnitude <= 0.1f)
        {
            isArrive = true;
            _time = 0;
            _aniReturn.SetInteger(_move, 0);
        }
        else if ((_navMove.destination - transform.position).magnitude < _col.bounds.size.x)
        {
            _time += Time.deltaTime;
        }

        if (_time > _maxReturnTime)
        {
            _time = 0;
            _aniReturn.SetInteger(_move, 0);
            return true;
        }

        return isArrive;
    }
    

    private void Flip()
    {
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
