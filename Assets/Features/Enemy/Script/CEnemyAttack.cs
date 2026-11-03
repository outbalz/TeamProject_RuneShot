using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CEnemyAttack : MonoBehaviour, IEnemyControllInterface
{
    #region 인스펙터
    [SerializeField] private CEnemy _enemy;
    [SerializeField] private Animator _aniAttack;
    #endregion

    #region 내부변수
    private float _attackInterval;
    private float _time;
    private string _attack = "TAttack";
    #endregion

    private void Awake()
    {
        if( _enemy == null )
        {
            _enemy = GetComponent<CEnemy>();
        }
        if( _aniAttack == null )
        {
            _aniAttack = GetComponent<Animator>();
        }

        _attackInterval = _enemy.Enemydata.WeaponFireInterval;
    }
     
    public void ActionEnter(Transform PlayerTr)
    {
        Debug.Log("Enter Attack");
    }
    public void ActionUpdate(Transform PlayerTr)
    {
         
        if (PlayerTr == null)
        {
            return;
        }

        if((PlayerTr.position - transform.position).x <= 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
         
         _time += Time.deltaTime;

        if( _time < _attackInterval )
        {
            return;
        }

        _aniAttack.SetTrigger(_attack);
        _time = 0;
         
    }

    public void ActionExit()
    {
        Debug.Log("Exit Attack");
        _time = 0; 
    }
    private void Rotation(Transform PlayerTr)
    { 
        Quaternion rot = Quaternion.Euler(90f,0 , 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(PlayerTr.position - transform.position) * rot, 0.2f);

        transform.eulerAngles = new Vector3(0,0, transform.eulerAngles.z);
    }
}
