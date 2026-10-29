using UnityEngine;

public class CPlayerHPStatusAdapter : CCharacterStatusHandler<CCharacterStatusDataSO>
{
    #region 인스펙터
    [SerializeField] private CPlayerStatusHandler _playerStatus;
    #endregion

    #region 프로퍼티
    public override float MaxHP => _playerStatus.MaxHP;
    #endregion
}
