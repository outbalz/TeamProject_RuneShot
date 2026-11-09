using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLootChestController : MonoBehaviour, IInteractable
{
    #region 인스팩터
    [SerializeField] private CItemContainerInventoryHandler _inventory;
    #endregion

    #region 내부변수
    private bool _isOpend = false;
    #endregion

    public void Interact()
    {
        if(!_isOpend)
        {
            if (_inventory == null)
            {
                Debug.LogWarning("인벤토리 핸들러 누락");
                return;
            }
            _isOpend = true;
        }

        throw new System.NotImplementedException();
    }
}
