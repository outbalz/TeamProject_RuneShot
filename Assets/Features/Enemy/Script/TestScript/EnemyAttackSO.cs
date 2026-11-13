using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Attack")]
public class EnemyAttackSO : EnemyBaseSO
{

    public override CEnemyBaseState createState(CEnemyControllerTest cEnemyController)
    {
        return new CEnemyAttackState(cEnemyController);
    }
}
