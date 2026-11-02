using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CEnemyAttack : MonoBehaviour, IEnemyControllInterface
{

    private float _attackInterval; 
    private float _time;

    private CEnemy _enemy;
    private Animator _aniAttack;

    private string _attack = "TAttack";

    private void Awake()
    {
        _enemy = GetComponent<CEnemy>();
        _aniAttack = GetComponent<Animator>();

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
