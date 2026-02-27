using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishedGUI : BaseGui
{
    [SerializeField] private Text scoreText;

    private static FinishedGUI _instance;

    public static FinishedGUI Get()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<FinishedGUI>();
        }
        return _instance;
    }

    void Start()
    {
        guiName = EPanel.FinishedPanel;
        //scoreText.text = "";
    }

    public void SetScoreText(string score)
    {
        Debug.Log("SetScoreText: " + score);
        scoreText.text = score;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void GoSelectedGUI()
    {
        DataBase.Get().Save(PlayerInfoHandle.Get().CurrentPlayerData);
        SceneManager.LoadSceneAsync("LoginDemo");
    }
}
