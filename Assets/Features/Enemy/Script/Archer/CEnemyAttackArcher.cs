using Cysharp.Threading.Tasks; 
using System.Threading; 
using UnityEngine;
using UnityEngine.AI; 

public class CEnemyAttackArcher : CEnemyAttack
{
     
    [SerializeField] private NavMeshAgent _Agent;
    [SerializeField] private float _runawayDistance = 2f;
    private Bool _acting = new(); 
    private CEnemyRunaway _runaway;
    private CancellationTokenSource _source;

    protected override void Awake()
    {
        base.Awake();
        iniRunaway(); 
    }
     
    public override void ActionUpdate(Transform PlayerTr)
    {

        if (PlayerTr == null || _playerHP == null)
        {
            return;
        }

        if(_acting._isActing)
        {
            return;
        }
         
        Flip(PlayerTr);

         _time += Time.deltaTime;

        if( _time < _attackInterval )
        {
            return;
        }

        if((PlayerTr.position - transform.position).magnitude < _runawayDistance)
        {
            _source = new CancellationTokenSource();
            _acting._isActing = true;
            _runaway.Runaway(PlayerTr, _runawayDistance, _source).Forget();
        }
        

        _playerHP.OnHit(_damage);

        _aniAttack.SetTrigger(_attack);
        _time = 0;

    }
    public override void ActionExit()
    {
        TokenCancel(); 
        Debug.Log("Exit Attack");
        _time = 0;
    }

    private void OnDestroy()
    {
        TokenCancel();
    }

    private void TokenCancel()
    {
        if (_source != null)
        {
            _source.Cancel();
            _source.Dispose();
            _source = null;
        }

    }

    void iniRunaway()
    {
        _runaway = new CEnemyRunaway(_aniAttack, _enemy, _Agent, transform, _acting);
    }
}

public class Bool
{
    public bool _isActing;
}
