
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAmmunitionType
{
    PistolRound,
    RifleRound,
    ShootgunShell
}

public static class CInventoryAmmunitionHandler
{
    //인벤토리에서 장전 요청
    //CInventoryAmmunitionHandler.ReloadFormInventory(out 실제로 장전될 탄약량(인벤토리의 탄약이 요청한 탄약보다 적을 경우 대비) );
    //인벤토리에 탄약이 없으면 false 반환, 있으면 true반환
    public static bool ReloadFormInventory(out int loadAmount)
    {
        throw new System.NotImplementedException();
    }

    //특정 탄약 아템을 직접 정해서 장전할 때 사용
    public static bool ReloadFormInventory(CItemDataSO ammoItem, out int loadAmount)
    {
        throw new System.NotImplementedException();
    }
}
