using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyStatusHandler : CCharacterStatusHandler<CEnemyStatusDataSO>
{
    #region 프로퍼티
    public float DetectionDistance => _dataSO.DetectionDistance;
    #endregion

}
