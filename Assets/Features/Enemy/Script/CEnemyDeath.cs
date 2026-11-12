using UnityEngine;

public class CEnemyDeath : CEnemyActionBase
{
    #region 인스펙터
    [SerializeField] Animator _aniDeath;
    #endregion

    #region 내부변수
    string _death = "BDeath";
    #endregion

    private void Awake()
    {
        if( _aniDeath == null )
        { 
            _aniDeath = GetComponent<Animator>();
        }
    }

    public override void ActionEnter(Transform PlayerTr)
    {
        _aniDeath.SetBool(_death,true);
    }
    public override void ActionUpdate(Transform PlayerTr)
    {
        if(_aniDeath.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            ActionExit();
        }
        Debug.Log("Death update");
    }
    public override void ActionExit()
    {
        Destroy(this.gameObject);
    }
}
