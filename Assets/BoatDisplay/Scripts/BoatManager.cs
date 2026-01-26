using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoatScaleState
{
    Small,
    Large
}

public class BoatManager : MonoBehaviour
{
    public List<BoatInfo> boats = new List<BoatInfo>();

    private int currBoatsIndex = 0;

    private static BoatManager install;

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        DisplayDiffBoatBaseOnIndex(currBoatsIndex, true);
    }

    public void DisplayDiffBoatBaseOnIndex(int index, bool active)
    {
        for (int i = 0; i < boats.Count; i++)
        {
            boats[i].SetActive(index == i && active);
        }
    }

    public void SetBoatScaleBaseOnIndex(int index, BoatScaleState state)
    {
        for (int i = 0; i < boats.Count; i++)
        {
            if (i != index) continue;
            Action action = state == BoatScaleState.Large ? boats[i].SetLargeScale : boats[i].SetSmallScale;
            action?.Invoke();
        }
    }

    public static BoatManager Get()
    {
        if (install == null)
        {
            install = FindObjectOfType<BoatManager>();
        }
        return install;
    }
}
