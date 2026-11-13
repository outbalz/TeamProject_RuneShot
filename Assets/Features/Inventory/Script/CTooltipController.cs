using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CTooltipController : CSingleton<CTooltipController>
{

    #region 인스팩터
    [SerializeField] private TMP_Text _header;
    [SerializeField] private TMP_Text _content;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _rootTransform;
    #endregion


    protected override void Awake()
    {
        base.Awake();

        if (_header == null || _content == null)
        {
            Debug.LogWarning("텍스트 요소 누락");
        }

        if (_rectTransform == null)
        {
            if (TryGetComponent<RectTransform>(out _rectTransform))
            {
                Debug.LogWarning("_rectTransform 누락");
            }
        }

        if (_rootTransform == null)
        {
            if (transform.parent.TryGetComponent<RectTransform>(out _rootTransform))
            {
                Debug.LogWarning("_rootTransform 누락");
            }
        }

        gameObject.SetActive(false);
    }

    public static void ShowTooltip(string header, string content)
    {
        if (Instance == null)
        {
            Debug.LogWarning("CTooltipController 인스턴스 누락");
            return;
        }

        Instance.gameObject.SetActive(true);
        Instance._header.text = header;
        Instance._content.text = content;
    }



    public static void HideTooltip()
    {
        if (Instance == null)
        {
            Debug.LogWarning("CTooltipController 인스턴스 누락");
            return;
        }

        Instance.gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        float pivotX = -0.1f;
        float pivotY = 1;

        if ((Screen.width - mousePos.x) / _rootTransform.localScale.x < _rectTransform.sizeDelta.x)
        {
            pivotX = 1;
        }

        if (mousePos.y / _rootTransform.localScale.y < _rectTransform.sizeDelta.y)
        {
            pivotY = 0;
        }

        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = mousePos;
    }

}