using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CEnemyAttack : MonoBehaviour, IEnemyControllInterface
{
    [SerializeField]
    private float _attackInterval = 3f; 

    private float _time; 
    public void ActionEnter(Transform PlayerTr)
    {
        ActionUpdate(PlayerTr);
    }
    public void ActionUpdate(Transform PlayerTr)
    {
        if (PlayerTr == null)
        {
            return;
        }

        Rotation(PlayerTr);

        _time += Time.deltaTime;

        if( _time < _attackInterval )
        {
            return;
        }

        Debug.Log("Attack");
        _time = 0;
    }

    public void ActionExit()
    {
        _time = 0;
    }
    private void Rotation(Transform PlayerTr)
    {
        Quaternion rot = Quaternion.Euler(0, -90f, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(PlayerTr.position - transform.position) * rot, 0.2f);
    }
}
