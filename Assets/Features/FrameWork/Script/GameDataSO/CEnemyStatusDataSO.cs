using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CharacterStatusDataSO/CEnemyStatusData", fileName = "EnemyStatusDataSO_")]
public class CEnemyStatusDataSO : CCharacterStatusDataSO
{
    #region 인스팩터
    [SerializeField] private float _detectionDistance;  //탐지 거리
    #endregion

    #region 프로퍼티
    public float DetectionDistance => _detectionDistance;
    #endregion
}
