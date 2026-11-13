using UnityEngine;

public class CEnemyFSM : MonoBehaviour 
{
    #region 인스펙터 
    [SerializeField] private CEnemy _enemy;
    #endregion

    #region 내부변수
    private CEnemyActionBase _currentControll;
    private CEnemyActionBase _preControll;
    #endregion
     
    private void Awake()
    {
        if(_enemy == null)
        { 
            _enemy = GetComponent<CEnemy>();
        }    
    }

    public void Action()
    {
        _currentControll.ActionUpdate(_enemy.PlayerTr);
    }

    public void SetControll(CEnemyActionBase controll)
    { 
        if (_currentControll == controll)
        {
            return;
        }
        
        _preControll = _currentControll;
        _currentControll = controll;

        if(_preControll != null)
        { 
            _preControll.ActionExit();
        }
         
        _currentControll.ActionEnter(_enemy.PlayerTr);
         
    }
     
}
