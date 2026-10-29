using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CharacterStatusDataSO/PlayerStausData", fileName = "PlayerStausDataSO_")]
public class CPlayerStausDataSO : CCharacterStatusDataSO
{
    #region 인스팩터
    [SerializeField] private float _maxHunger;

    //==========vv멀티플라이어, 곱해서사용vv========
    [SerializeField] private float _hungerRate = 1;
    [SerializeField] private float _detectionDistance = 1;  //피탐지 거리
    //===========^^멀티플라이어^^================
    #endregion

    #region 프로퍼티
    public float MaxHunger => _maxHunger;
    public float HungerRate => _hungerRate;
    public float DetectionDistance => _detectionDistance;
    #endregion
}
