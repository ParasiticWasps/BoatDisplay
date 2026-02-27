using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoHandle : MonoBehaviour
{
    private static PlayerInfoHandle instance;

    public static PlayerInfoHandle Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<PlayerInfoHandle>();
        }
        return instance;
    }

    public UserData CurrentPlayerData;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void UpdatePlayerScore(int score)
    {
        int maxScore = Mathf.Max(CurrentPlayerData.Score, score);
        Debug.Log($"UpdatePlayerScore: {score}, maxScore: {maxScore}");
        CurrentPlayerData.Score = maxScore;
    }
}
