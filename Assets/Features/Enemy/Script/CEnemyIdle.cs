using UnityEngine;

public class CEnemyIdle : MonoBehaviour, IEnemyControllInterface
{
    #region 인스펙터
    [SerializeField] private Animator _aniIdle;
    #endregion

    #region 내부변수 
    private string _move = "IMove";
    #endregion

    private void Awake()
    { 
        if(_aniIdle == null)
        {
            _aniIdle = GetComponent<Animator>();
        } 
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
