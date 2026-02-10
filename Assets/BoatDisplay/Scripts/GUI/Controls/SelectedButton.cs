using BoatDisplay;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    [SerializeField] private Color selectedColor;

    [SerializeField] private Color disselectedColor;

    [SerializeField] private Image bottomImage;

    [SerializeField] private BaseGui linkGui;

    [SerializeField] private Mode mode;

    private Text buttonName;

    private Button button;

    public event Action<Mode> OnSelectedEvent;

    private void Awake()
    {
        buttonName = GetComponentInChildren<Text>();
        button = GetComponent<Button>();

        Initialized();
    }

    private void Initialized()
    {
        button.onClick.AddListener(() => 
        {
            OnClickedButton();
            OnSelectedEvent?.Invoke(mode);
        });

        DisselectedButtonEvent();
    }

    public void OnClickedButton()
    {
        buttonName.color = selectedColor;

        bottomImage.gameObject.SetActive(true);
        linkGui.gameObject.SetActive(true);
    }

    public void DisselectedButtonEvent()
    {
        buttonName.color = disselectedColor;

        bottomImage.gameObject.SetActive(false);
        linkGui.gameObject.SetActive(false);
    }

    public Mode GetMode() => mode;
}
