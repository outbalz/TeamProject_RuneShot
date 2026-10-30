using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyIdle : MonoBehaviour, IEnemyControllInterface
{
   
    public void ActionEnter(Transform PlayerTr)
    {
        
    }
    public void ActionUpdate(Transform PlayerTr)
    {
        Debug.Log("Idle"); 
    }

    public void ActionExit()
    {

    }
}
