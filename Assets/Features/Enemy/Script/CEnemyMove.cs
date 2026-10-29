using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CEnemyMove : MonoBehaviour,IEnemyControllInterface
{
    [SerializeField] float _speed = 4f;
    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
    public void ActionEnter(Transform PlayerTr)
    {
        ActionUpdate(PlayerTr);
    }
    public void ActionUpdate(Transform PlayerTr)
    {
        Debug.Log("Move");

        if (PlayerTr == null)
        {
            return;
        }
     
        Vector3 dir = Vector3.Normalize(PlayerTr.position - transform.position);

        _rb2D.velocity = dir * _speed;

        Rotation(PlayerTr);

        
    }

    public void ActionExit()
    {
        _rb2D.velocity = Vector3.zero;
    }

    private void Rotation(Transform PlayerTr)
    {
        Quaternion rot = Quaternion.Euler(0, -90f, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(PlayerTr.position - transform.position) * rot, 0.2f);
    }

    
}
