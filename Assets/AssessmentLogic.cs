using Enviro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricFogAndMist;

public class AssessmentLogic : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        EnviroManager.instance.Time.SetTimeOfDay(0.0f);
        VolumetricFog.instance.alpha = 0.0f;

        int randindex = BoatManager.Get().GetRandomBoatsIndex();
        BoatManager.Get().DisplayDiffBoatBaseOnIndex(randindex, true);
    }
}
