using DG.Tweening;
using Enviro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VolumetricFogAndMist;

public class BoatsLightManager : MonoBehaviour
{
    [SerializeField] private List<LightBase> lights = new List<LightBase>();

    private bool isOpenLights = false;

    private void Awake()
    {
        lights = GetComponentsInChildren<LightBase>(true).ToList();
        //Debug.Log();
        SetLightsIntensity(false);
    }

    private void FixedUpdate()
    {
        if (CheckLightsNeedToBeTurnedOn() != isOpenLights)
        {
            isOpenLights = !isOpenLights;
            SetLightsIntensity(isOpenLights);
        }
    }

    private bool CheckLightsNeedToBeTurnedOn()
    {
        return VolumetricFog.instance.alpha >= 0.9f || EnviroManager.instance.Time.hours < 6.0f || EnviroManager.instance.Time.hours >= 20.0f;
    }

    private void SetLightsIntensity(bool isOpen)
    {
        for (int i = 0; i < lights.Count; i++)
        {
            int index = i;
            lights[index].gameObject.SetActive(isOpen);
        }
    }
}
