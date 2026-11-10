
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDataBaseManager : CSingletonDontDstroyAtLoad<CDataBaseManager>
{
	#region 인스팩터
	[SerializeField] private CWeaponDatabaseSO _weaponDB;
	[SerializeField] private CPrefabDatabaseSO _prefabDB;
	[SerializeField] private CEnchantCraftingRecipeDatabaseSO _enchantCraftingRecipeDB;
	[SerializeField] private CLootTableDatabaseSO _lootTableDB;
    #endregion

    #region 
    public CWeaponDatabaseSO WeaponDB => _weaponDB;
	public CPrefabDatabaseSO PrefabDB => _prefabDB;
	public CEnchantCraftingRecipeDatabaseSO EnchantCraftingRecipeDB => _enchantCraftingRecipeDB;
	public CLootTableDatabaseSO LootTableDB => _lootTableDB;
    #endregion

}
