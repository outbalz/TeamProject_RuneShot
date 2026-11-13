using UnityEngine;

public class CEnemyIdle : CEnemyActionBase
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
    public override void ActionEnter(Transform PlayerTr)
    {
        Debug.Log("Enter Idle");
         
        _aniIdle.SetInteger(_move, 0);
    }
    
    public override void ActionExit()
    {
        Debug.Log("Exit Idle"); 
    }
     
}
