using DG.Tweening;
using Enviro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricFogAndMist;

public class WeatherGUI : BaseGui
{
    [SerializeField] private List<WeatherButton> weatherButtons = new List<WeatherButton>();

    WeatherButton lastoneButton;

    //[SerializeField] private VolumetricFogPosT fogLauncher;

    private static WeatherGUI instance;

    public static WeatherGUI Get()
    {
        if (instance == null)
        {
            instance = FindAnyObjectByType<WeatherGUI>();
        }
        return instance;
    }

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

            if (lastoneButton.WeatherIndex == 3)
            {
                EnviroManager.instance.Time.SetTimeOfDay(EnviroManager.instance.Time.FogLastTimeOfDay);
            }
        }

        lastoneButton = weatherButton;
        DOTween.To(() => VolumetricFog.instance.alpha, x => VolumetricFog.instance.alpha = x,
            lastoneButton.WeatherIndex == 3 ? 0.714f : 0.0f, 1.5f);

        StartCoroutine(SelectedWeatherButtonCoroutine());
        //fogLauncher.enabled = lastoneButton.WeatherIndex == 3;
    }

    private IEnumerator SelectedWeatherButtonCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        if (lastoneButton.WeatherIndex == 3)
        {
            EnviroManager.instance.Time.FogLastTimeOfDay = EnviroManager.instance.Time.GetTimeOfDay();
            EnviroManager.instance.Time.SetTimeOfDay(3.0f);
            EnviroManager.instance.Time.SetTimeOfDay(0.0f);
        }
    }
}
