using UnityEngine;

public class CEnemy : MonoBehaviour
{
    #region 인스펙터 
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CEnemyDetect _detectObject;
    [SerializeField]
    private CEnemyStatusDataSO _enemydata;
    #endregion

    #region 내부변수
    private Transform _playerTr; 
    //private string _stringDamaged = "TDamaged";
   // protected float _hp;
    protected bool _isAlive = true; 
    #endregion

    #region 프로퍼티
    public CEnemyStatusDataSO Enemydata => _enemydata;
    public Transform PlayerTr { get { return _playerTr; } set { _playerTr = value; } }
    public bool IsAlive => _isAlive;
    #endregion

    protected virtual void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        Debug.Log("디텍트 오브젝트 생성");
        //Instantiate(_detectObject, transform.position + -transform.right/2 * _enemydata.DetectionDistance, transform.rotation,transform); 
        Instantiate(_detectObject,transform);
        //_hp = _enemydata.MaxHP;
         
    }
   //public void GetDamaged(float damage)
   //{
   //    _hp -= damage;
   //    _animator.SetTrigger(_stringDamaged);
   //}
    public bool Die()
    {
       //if (_hp <= 0)
       //{
       //    _hp = 0;
       //
            _isAlive = false;
        //}    
        return _isAlive;
    }
}
