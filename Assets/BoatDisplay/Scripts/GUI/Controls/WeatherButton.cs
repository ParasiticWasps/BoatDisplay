using Enviro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherButton : MonoBehaviour
{
    [SerializeField] private Sprite defaultImage;

    [SerializeField] private Sprite selectedImage;

    public int WeatherIndex = 0;

    #region UI Component

    private Image iconImage;

    private Button button;

    #endregion

    public event Action OnChangedWeatherEvent;

    public event Action<WeatherButton> OnButtonClickedEvent;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(() => 
        {
            OnClickedEvent();
            OnChangedWeatherEvent?.Invoke();
            OnButtonClickedEvent?.Invoke(this);
        });
    }

    public void OnClickedEvent()
    {
        iconImage.sprite = selectedImage;

        if (EnviroManager.instance.Weather != null)
        {
            if (EnviroManager.instance.Weather.Settings.weatherTypes.Count >= WeatherIndex)
                EnviroManager.instance.Weather.ChangeWeather(EnviroManager.instance.Weather.Settings.weatherTypes[WeatherIndex]);
        }
    }

    public void OnDisClickedEvent()
    {
        iconImage.sprite = defaultImage;
    }
}
