using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseSO : ScriptableObject
{ 
    public abstract CEnemyBaseState createState(CEnemyControllerTest cEnemyController);
}
