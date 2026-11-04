using UnityEngine;

public class CEnemyHPAdapter : CCharacterStatusHandler<CCharacterStatusDataSO>
{
    #region 인스펙터
    [SerializeField] private CEnemyStatusHandler _enemyStatus;
    #endregion

    #region 프로퍼티
    public override float MaxHP => _enemyStatus.MaxHP;
    #endregion
}