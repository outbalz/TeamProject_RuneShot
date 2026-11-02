using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "RuneShot/Weapon/Basic Projectile Data")]
public class CBasicProjectileDataSO : CProjectileDataSO
{
    public override UniTask OnImpact()
    {
        return UniTask.CompletedTask;
    }

    public override UniTask OnHit()
    {
        return UniTask.CompletedTask;
    }
}
