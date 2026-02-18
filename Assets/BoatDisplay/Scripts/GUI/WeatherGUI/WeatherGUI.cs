using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricFogAndMist;

public class WeatherGUI : BaseGui
{
    [SerializeField] private List<WeatherButton> weatherButtons = new List<WeatherButton>();

    WeatherButton lastoneButton;

    //[SerializeField] private VolumetricFogPosT fogLauncher;

    private void Start()
    {
        for (int i = 0; i < weatherButtons.Count; i++)
        {
            int idx = i;
            weatherButtons[idx].OnButtonClickedEvent += SelectedWeatherButton;
        }
        //fogLauncher.enabled = false;

        VolumetricFog.instance.alpha = 0.0f;
    }

    private void SelectedWeatherButton(WeatherButton weatherButton)
    {
        if (lastoneButton != null)
        {
            lastoneButton.OnDisClickedEvent();
        }

        lastoneButton = weatherButton;
        DOTween.To(() => VolumetricFog.instance.alpha, x => VolumetricFog.instance.alpha = x,
            lastoneButton.WeatherIndex == 3 ? 1.0f : 0.0f, 1.5f);
        //fogLauncher.enabled = lastoneButton.WeatherIndex == 3;
    }
}
