using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class CEnemyFSM : CEnemy
{
       
    private IEnemyControllInterface _currentControll;
    private IEnemyControllInterface _preControll; 
     
    public void Action()
    {
        _currentControll.ActionUpdate(PlayerTr);
    }

    public void SetControll(IEnemyControllInterface controll)
    {
        if(_currentControll == controll)
        {
            return;
        }

        _preControll = _currentControll;
        _currentControll = controll;

        if(_preControll != null)
        { 
            _preControll.ActionExit();
        }
         
        _currentControll.ActionEnter(PlayerTr);
         
    }
     
}
