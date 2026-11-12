using UnityEngine;
using UnityEngine.AI; 

public class CEnemyMove : CEnemyActionBase
{
    #region 인스펙터
    [SerializeField] private CEnemy _enemy;
    [SerializeField] private NavMeshAgent _navMove;
    [SerializeField] private Animator _aniMove;
    #endregion

    #region 내부변수 
    private string _move = "IMove";
    
    private float _time;
    private float _waitTime = 3f;
    private Vector3 _setPosition;  
    private Vector3 _prePlayerPosision;
    #endregion

    private void Awake()
    {
        if( _enemy == null || _navMove == null || _aniMove == null)
        {
            Debug.Log("인스펙터 확인");
        }
          
        _navMove.speed = _enemy.Enemydata.MovementSpeed;
        _setPosition = transform.position;
    }
    
    public override void ActionEnter(Transform PlayerTr)
    {
        Debug.Log("Enter Move");

        _navMove.stoppingDistance = _enemy.Enemydata.AttackRange;

        _aniMove.SetInteger(_move, 1);
    }
    public override void ActionUpdate(Transform PlayerTr)
    { 
        if (PlayerTr == null)
        {
            return;
        }

        if (_navMove.speed != 0f)
        {
            float speed = _navMove.velocity.sqrMagnitude / (_navMove.speed * _navMove.speed);

            if (speed == 0)
            {
                _time += Time.deltaTime;
            }

            _aniMove.speed = speed == 0 ? 1f : speed;
        }

        if (_prePlayerPosision == PlayerTr.position)
        {
            if (_time <= _waitTime)
            {
                return;
            }
           
        }
         
        _prePlayerPosision = PlayerTr.position;


        if (!_navMove.pathPending)
        {
            if (_time <= _waitTime)
            {
                _navMove.destination = PlayerTr.position;

            }
            else
            {
                _time = 0f;
                _navMove.stoppingDistance = 0;
                _navMove.destination = _setPosition; 
            }
        }

        Flip(); 
    }
  
    public override void ActionExit()
    {
        _aniMove.SetInteger(_move, 0);
        _aniMove.speed = 1f;
        _time = 0f;
        _navMove.stoppingDistance = _enemy.Enemydata.AttackRange;
        Debug.Log("Exit Move"); 
        
    }

    private void Flip()
    { 
        
        if ((_navMove.destination - transform.position).x <= 0)
        { 
           transform.rotation = Quaternion.identity;
        }
        else
        { 
           transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }
}
