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

    public delegate void OnDisplayButtonClicked(int boatIndex);

    private event OnDisplayButtonClicked onDisplayShipEvent;

    private event OnDisplayButtonClicked onDisplayBoatEvent;

    #endregion
    public void Initialized(string boatName, int _boatIndex, OnDisplayButtonClicked displayShip, OnDisplayButtonClicked displayBoat)
    {
        nameText.text = boatName;
        boatIndex = _boatIndex;
        onDisplayShipEvent = displayShip;
        onDisplayBoatEvent = displayBoat;
    }

    #region UI Components Unity Event

    public void OnClickedExpandButton()
    {
        ControlPanel(isExpand);
    }

    public void ShowBigShip()
    {
        onDisplayShipEvent(boatIndex);
    }

    public void ShowBoat()
    {
        onDisplayBoatEvent(boatIndex);
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
}
 