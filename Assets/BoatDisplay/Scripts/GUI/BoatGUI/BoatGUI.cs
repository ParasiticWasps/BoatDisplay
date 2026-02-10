using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatGUI : BaseGui
{
    [SerializeField] private GameObject boatItemPrefab;

    [SerializeField] private Transform boatItemSpawn;

    private List<BoatButton> boatButtons = new List<BoatButton>();

    private BoatManager boatManager;

    public Button displayButton;

    public BoatPanel boatPanel;

    override public void Awake()
    {
        guiName = EPanel.BoatPanel;
        base.Awake();
        Initialized();
    }

    private void Start()
    {
        displayButton.onClick.AddListener(OnClickDisplayButton);
    }

    private void Initialized()
    {
        boatButtons.Clear();
        boatManager = BoatManager.Get();
        for (int i = 0; i < boatManager.boats.Count; i++)
        {
            int index = i;
            BoatInfo boatInfo = boatManager.boats[i];
            GameObject boatItem = Instantiate(boatItemPrefab, boatItemSpawn);
            BoatButton boatButton = boatItem.GetComponent<BoatButton>();
            boatButton.Initialized(boatInfo.boatName, index);
            boatButtons.Add(boatButton);
            LoopScroll.Get().ItemList.Add(boatButton.transform);
        }

        boatPanel.OnBoatClickEvent += DisplayBoat;
        boatPanel.OnShipClickEvent += DisplayShip;
    }

    private void DisplayShipEvent(int boatIndex)
    {
        boatManager.DisplayDiffBoatBaseOnIndex(boatIndex, true);
        boatManager.SetBoatScaleBaseOnIndex(boatIndex, BoatScaleState.Large);
    }

    private void DisplayBoatEvent(int boatIndex)
    {
        boatManager.DisplayDiffBoatBaseOnIndex(boatIndex, true);
        boatManager.SetBoatScaleBaseOnIndex(boatIndex, BoatScaleState.Small);
    }

    private void DisplayShip()
    {
        int boatIndex = LoopScroll.Get().TempCentre.GetComponentInChildren<BoatButton>().GetBoatIndex();
        DisplayShipEvent(boatIndex);
    }

    private void DisplayBoat()
    {
        int boatIndex = LoopScroll.Get().TempCentre.GetComponentInChildren<BoatButton>().GetBoatIndex();
        DisplayBoatEvent(boatIndex);
    }

    private void SetBoatPanelActive(bool active)
    {
        boatPanel.SetActive(active);
    }

    private void OnClickDisplayButton()
    {
        bool active = !boatPanel.gameObject.activeSelf;
        SetBoatPanelActive(active);
    }
}
