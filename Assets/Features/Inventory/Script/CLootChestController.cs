using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CItemContainerInventoryHandler))]
public class CLootChestController : MonoBehaviour, IInteractable
{
    #region 인스팩터
    [SerializeField] private CLootChestDataSO _data;
    [SerializeField] private CItemContainerInventoryHandler _inventory;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    #endregion

    #region 내부변수
    private bool _isOpend = false;
    #endregion

    private void Awake()
    {
        if(_inventory == null)
        {
            if(!TryGetComponent<CItemContainerInventoryHandler>(out _inventory))
            {
                Debug.LogWarning("아이템 컨테이너 인벤토리 누락");
                enabled = false;
                return;
            }
        }

        if(_spriteRenderer == null)
        {
            if(!TryGetComponent<SpriteRenderer>(out _spriteRenderer))
            {
                Debug.LogWarning("스프라이트 렌더러 누락");
                enabled = false;
                return;
            }
        }


        if(_data == null)
        {
            Debug.LogWarning("CLootChestDataSO 누락");
            enabled = false;
            return;
        }

        _spriteRenderer.sprite = _data.ChestSprite;
    }

    public void Interact()
    {
        if(!_isOpend)
        {
            if (_inventory == null)
            {
                Debug.LogWarning("인벤토리 핸들러 누락");
                return;
            }

            _inventory.InitializeInventory(_data.InventorySize);

            IRewardable[] itemRewards = CLootTableManager.GetLootTableElements(_data.LootTableID, _inventory.InventorySize);

            for (int i = 0; i < itemRewards.Length; i++)
            {
                if (!(itemRewards[i] is CItemReward itemReward))
                {
                    continue;
                }

                if(itemReward.Item == null)
                {
                    continue;
                }

                int amount = Random.Range(itemReward.MinAmount, itemReward.MaxAmount +1);

                _inventory.AddToInventory(itemReward.Item, amount);
            }

            _isOpend = true;
        }

        CLootChestUIController.Instance.ToggleUI(true);
        CLootChestUIController.Instance.UpdateSlot(_inventory);

    }

    //for test=================

    [ContextMenu("DebuggingTest(OpenChest)")]
    private void ChestOpenTest()
    {
        _isOpend = false;
        Interact();
    }

}
