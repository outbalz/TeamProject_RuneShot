using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDeath : MonoBehaviour, IEnemyControllInterface
{
    Animator _aniDeath;

    string _death = "BDeath";
    private void Awake()
    {
        _aniDeath = GetComponent<Animator>();
    }

    public void ActionEnter(Transform PlayerTr)
    {
        _aniDeath.SetBool(_death,true);
    }
    public void ActionUpdate(Transform PlayerTr)
    {
        if(_aniDeath.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            ActionExit();
        }
        Debug.Log("Death update");
    }
    public void ActionExit()
    {
        Destroy(this.gameObject);
    }
}
