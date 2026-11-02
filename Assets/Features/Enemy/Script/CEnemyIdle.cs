using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyIdle : MonoBehaviour, IEnemyControllInterface
{
    
    private Animator _aniIdle;
     
    private string _move = "IMove";
      
    private void Awake()
    { 
        _aniIdle = GetComponent<Animator>(); 
    }
    public void ActionEnter(Transform PlayerTr)
    {
        Debug.Log("Enter Idle");
         
        _aniIdle.SetInteger(_move, 0);
    }
    public void ActionUpdate(Transform PlayerTr)
    { 

    }

    public void ActionExit()
    {
        Debug.Log("Exit Idle"); 
    }
     
}
