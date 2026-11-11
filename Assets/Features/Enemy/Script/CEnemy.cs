using UnityEngine;

public class CEnemy : MonoBehaviour
{
    #region 인스펙터 
    [SerializeField]
    private CEnemyDetect _detectObject;
    [SerializeField]
    private CEnemyStatusHandler _enemydata;
    #endregion

    #region 내부변수
    private Transform _playerTr; 
    protected bool _isAlive = true; 
    #endregion

    #region 프로퍼티
    public CEnemyStatusHandler Enemydata => _enemydata;
    public Transform PlayerTr { get { return _playerTr; } set { _playerTr = value; } }
    public bool IsAlive => _isAlive;
    #endregion

    protected virtual void Awake()
    {
        Debug.Log("디텍트 오브젝트 생성");
        //Instantiate(_detectObject, transform.position + -transform.right/2 * _enemydata.DetectionDistance, transform.rotation,transform); 
        Instantiate(_detectObject,transform);
    }
  
    public bool Die()
    {
        _isAlive = false;
          
        return _isAlive;
    }
}
