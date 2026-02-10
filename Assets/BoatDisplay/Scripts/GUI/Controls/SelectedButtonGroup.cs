using BoatDisplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedButtonGroup : MonoBehaviour
{
    [SerializeField] private List<SelectedButton> selectedButtons = new List<SelectedButton>();

    private void Start()
    {
        AnyButtonOnClicked(Mode.Weather);

        foreach (var button in selectedButtons)
        {
            button.OnSelectedEvent += AnyButtonOnClicked;
        }
    }

    private void AnyButtonOnClicked(Mode mode)
    {
        foreach (var button in selectedButtons)
        {
            Action action = button.GetMode() != mode ? 
                () => button.DisselectedButtonEvent() : 
                () =>button.OnClickedButton();

            action?.Invoke();
        }
    }
}
