using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CMouseOverTriggerBlockHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CInputDispatcher.Instance.ToggleAction("TriggerWeapon", false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CInputDispatcher.Instance.ToggleAction("TriggerWeapon", true);
    }
}
