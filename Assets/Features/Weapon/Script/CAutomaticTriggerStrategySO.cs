using UnityEngine;

[CreateAssetMenu(menuName = "RuneShot/Weapon/Automatic Trigger")]
public class CAutomaticTriggerStrategySO : CSingleShotTriggerStrategySO
{
    public override bool IsAutomatic => true;
}
