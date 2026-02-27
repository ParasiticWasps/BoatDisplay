using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatPanel : MonoBehaviour
{
    [SerializeField] private Button shipButton;

    [SerializeField] private Button boatButton;

    public event Action OnBoatClickEvent;

    public event Action OnShipClickEvent;

    private void Start()
    {
        shipButton.onClick.AddListener(() => OnShipClickEvent?.Invoke());
        boatButton.onClick.AddListener(() => OnBoatClickEvent?.Invoke());

        SetActive(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
