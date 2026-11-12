using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine; 

public class CEnemyAttackArcher : CEnemyAttack
{
   
    public override void ActionUpdate(Transform PlayerTr)
    {

        if (PlayerTr == null || _playerHP == null)
        {
            return;
        }

        Flip(PlayerTr);

         _time += Time.deltaTime;

        if( _time < _attackInterval )
        {
            return;
        }

        _playerHP.OnHit(_damage);

        _aniAttack.SetTrigger(_attack);
        _time = 0;
         
    }

}
