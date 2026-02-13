using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherGUI : BaseGui
{
    [SerializeField] private List<WeatherButton> weatherButtons = new List<WeatherButton>();

    WeatherButton lastoneButton;

    private void Start()
    {
        for (int i = 0; i < weatherButtons.Count; i++)
        {
            int idx = i;
            weatherButtons[idx].OnButtonClickedEvent += SelectedWeatherButton;
        }
    }

    private void SelectedWeatherButton(WeatherButton weatherButton)
    {
        if (lastoneButton != null)
        {
            lastoneButton.OnDisClickedEvent();
        }
        lastoneButton = weatherButton;
    }
}
