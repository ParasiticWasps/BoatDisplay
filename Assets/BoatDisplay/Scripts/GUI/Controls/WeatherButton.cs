using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherButton : MonoBehaviour
{
    [SerializeField] private Sprite defaultImage;

    [SerializeField] private Sprite selectedImage;

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
    }

    public void OnDisClickedEvent()
    {
        iconImage.sprite = defaultImage;
    }
}
