using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatButton : MonoBehaviour
{
    [SerializeField] private GameObject boatPanel;

    [SerializeField] private VerticalLayoutGroup layout;

    [SerializeField] private Text nameText;

    private bool isExpand = true;

    public bool IsExpand { get => isExpand; }

    private int boatIndex = 0;

    #region Event

    #endregion

    public void Initialized(string boatName, int _boatIndex)
    {
        nameText.text = boatName;
        boatIndex = _boatIndex;
    }

    #region UI Components Unity Event

    public void OnClickedExpandButton()
    {
        ControlPanel(isExpand);
    }

    #endregion

    /// <summary>
    /// 控制面板展开与收起
    /// </summary>
    public void ControlPanel(bool _isExpand)
    {
        boatPanel.gameObject.SetActive(_isExpand);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        isExpand = !_isExpand;
    }

    public int GetBoatIndex() => boatIndex;
}
 