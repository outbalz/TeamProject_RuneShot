using UnityEngine;
using UnityEngine.AI; 

public class CEnemyMove : MonoBehaviour,IEnemyControllInterface
{
    #region 인스펙터
    [SerializeField] private CEnemy _enemy;
    [SerializeField] private NavMeshAgent _navMove;
    [SerializeField] private Animator _aniMove;
    #endregion

    #region 내부변수 
    private string _move = "IMove";
    #endregion

    private void Awake()
    {
        if( _enemy == null )
        {
            _enemy = GetComponent<CEnemy>();
        }
        if( _navMove == null )
        {
            _navMove = GetComponent<NavMeshAgent>();
        }
        if(_aniMove == null )
        {
            _aniMove = GetComponent<Animator>();
        }  

        _navMove.speed = _enemy.Enemydata.MovementSpeed; 
    }
    
    public void ActionEnter(Transform PlayerTr)
    {
        Debug.Log("Enter Move");

        _navMove.stoppingDistance = _enemy.Enemydata.AttackRange;

        _aniMove.SetInteger(_move, 1);
    }
    public void ActionUpdate(Transform PlayerTr)
    { 
        if (PlayerTr == null)
        {
            return;
        }
         
        _navMove.destination = PlayerTr.position;
 
        if(_navMove.speed != 0f)
        {
            _aniMove.speed = _navMove.velocity.magnitude / _navMove.speed;
        } 

        Flip(PlayerTr); 
    }
  
    public void ActionExit()
    {
        _aniMove.SetInteger(_move, 0);
        _aniMove.speed = 1f;
        Debug.Log("Exit Move");
    }

    private void Flip(Transform PlayerTr)
    {
        if ((PlayerTr.position - transform.position).x <= 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }
}
