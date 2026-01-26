using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatGUI : MonoBehaviour
{
    [SerializeField] private GameObject boatItemPrefab;

    [SerializeField] private Transform boatItemSpawn;

    private List<BoatButton> boatButtons = new List<BoatButton>();

    private BoatManager boatManager;

    private void Start()
    {
        Initialized();
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
            boatButton.Initialized(boatInfo.boatName, index, DisplayShipEvent, DisplayBoatEvent);
            boatButtons.Add(boatButton);
        }
    }

    public void DisplayShipEvent(int boatIndex)
    {
        boatManager.DisplayDiffBoatBaseOnIndex(boatIndex, true);
        boatManager.SetBoatScaleBaseOnIndex(boatIndex, BoatScaleState.Large);
    }

    public void DisplayBoatEvent(int boatIndex)
    {
        boatManager.DisplayDiffBoatBaseOnIndex(boatIndex, true);
        boatManager.SetBoatScaleBaseOnIndex(boatIndex, BoatScaleState.Small);
    }
}
