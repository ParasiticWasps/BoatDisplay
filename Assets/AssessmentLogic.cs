using Enviro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricFogAndMist;

public class AssessmentLogic : MonoBehaviour
{
    private int currAnswerIndex = -1; // 当前答案索引

    private int currScore = 0; // 当前得分

    private static AssessmentLogic instance;

    public static AssessmentLogic Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<AssessmentLogic>();
        }
        return instance;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        EnviroManager.instance.Time.SetTimeOfDay(0.0f);
        VolumetricFog.instance.alpha = 0.0f;
        SetQuestion();
    }

    /// <summary>
    /// 出题
    /// </summary>
    public void SetQuestion()
    {
        if (BoatManager.Get().GetTopicCount() == 0)
        {
            //GuiManager.Get().SetPanlesActive((int)EPanel.FinishedPanel);
            AnswerSheetGUI.Get().CompleteTheAnswer(currScore.ToString());
            PlayerInfoHandle.Get().UpdatePlayerScore(currScore);
            currScore = 0;
        }

        int randindex = BoatManager.Get().GetRandomBoatsIndex();
        currAnswerIndex = randindex;
        BoatManager.Get().DisplayDiffBoatBaseOnIndex(randindex, true);
    }

    public void ConfirmTheAnswer(int boatIndex)
    {
        currScore += boatIndex == currAnswerIndex ? 1 : 0;

        // 继续出题
        SetQuestion();
    }
}
