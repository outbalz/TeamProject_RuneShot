using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CEnemyHPHandler : CHPHandler<CEnemyStatusDataSO>
{
    #region 인스펙터
    [SerializeField] private CEnemy _enemy;
    [SerializeField] private Animator _aniHP;
    #endregion

    #region 내부변수
    private string _death = "BDeath";
    private string _stringDamaged = "TDamaged";
    #endregion

    private void Awake()
    {
        if(_enemy == null || _aniHP == null || _status == null)
        {
            Debug.Log("인스펙터 확인");
        }
         
        HP = MaxHP;

        OnDeath += DropItem;
        OnDeath += Destroy;

    }

    private void DropItem()
    {
       
    }
     
    private void Destroy()
    {
        Destroy(gameObject);
    }

    //TODO
    //invoke에 파티클, 사운드, 경험치, 아이템, destroy 구현 필요
    public override async UniTask OnHit(float damage)
    {
        if(damage <= 0 || HP <= 0)
        {
            return;
        }
        HP -= damage;
        OnHitInvoke();
        _aniHP.SetTrigger(_stringDamaged);
        if (HP <= 0f)
        {
            _enemy.Die();
            _aniHP.SetBool(_death,true);

            await UniTask.WaitUntil( () => _aniHP.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f,PlayerLoopTiming.Update, destroyCancellationToken);

            OnDeathInvoke();
             
        }
       
    }
    public override UniTask OnHeal(float amount)
    { 
        HP += amount;
        return UniTask.CompletedTask;
    }
}
