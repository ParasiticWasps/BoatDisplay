using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerSheetGUI : BaseGui
{
    [SerializeField] private GameObject boatItemPrefab;

    [SerializeField] private Transform boatItemSpawn;

    private List<BoatButton> boatButtons = new List<BoatButton>();

    private BoatManager boatManager;

    public Button displayButton;

    public BoatPanel boatPanel;

    [SerializeField] private FinishedGUI finishedGUI;

    private static AnswerSheetGUI instance;

    public static AnswerSheetGUI Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<AnswerSheetGUI>();
        }

        return instance;
    }

    override public void Awake()
    {
        guiName = EPanel.AnswerSheet;
        base.Awake();
        Initialized();
    }

    private void Start()
    {
        displayButton.onClick.AddListener(OnClickDisplayButton);
    }

    private void Initialized()
    {
        boatButtons.Clear();
        boatManager = BoatManager.Get();
        for (int i = 0; i < boatManager.boats.Count; i++)
        {
            int index = i;
            BoatInfo boatInfo = boatManager.boats[i];
            GameObject boatItem = Instantiate(boatItemPrefab, boatItemSpawn);
            BoatButton boatButton = boatItem.GetComponent<BoatButton>();
            boatButton.Initialized(boatInfo.boatName, index);
            boatButtons.Add(boatButton);
            LoopScroll.Get().ItemList.Add(boatButton.transform);
        }

        // TODO..
        boatPanel.OnShipClickEvent += ConfirmTheAnswer;
        //boatPanel.OnShipClickEvent += DisplayShip;
    }

    private void SetBoatPanelActive(bool active)
    {
        boatPanel.SetActive(active);
    }

    private void OnClickDisplayButton()
    {
        bool active = !boatPanel.gameObject.activeSelf;
        SetBoatPanelActive(active);
    }

    private void ConfirmTheAnswer()
    {
        int boatIndex = LoopScroll.Get().TempCentre.GetComponentInChildren<BoatButton>().GetBoatIndex();
        AssessmentLogic.Get().ConfirmTheAnswer(boatIndex);
    }

    public void CompleteTheAnswer(string score)
    {
        gameObject.SetActive(false);
        finishedGUI.SetActive(true);
        finishedGUI.SetScoreText(score);
    }
}
