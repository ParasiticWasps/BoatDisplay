using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginLogic : MonoBehaviour
{
    private static LoginLogic instance;

    public static LoginLogic Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<LoginLogic>();
        }
        return instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoginRequest(string account, string pwd, Action successAction, Action failedAction)
    {
        if (!DataBase.Get().Exists(account))
        {
            RegisterRequest(account, pwd);
            successAction?.Invoke();
            return;
        }
        
        UserData data = DataBase.Get().GetData(account);
        if (data.Password == pwd)
        {
            PlayerInfoHandle.Get().CurrentPlayerData = data;
            successAction?.Invoke();
            return;
        }

        failedAction?.Invoke();
        return;
    }

    public void RegisterRequest(string account, string pwd)
    {
        UserData data = new UserData();
        data.Account = account;
        data.Password = pwd;
        PlayerInfoHandle.Get().CurrentPlayerData = data;
        DataBase.Get().Save(data);
    }
}


[Serializable]
public class UserData
{
    [SerializeField]
    public string Account;

    [SerializeField]
    public string Password;

    [SerializeField]
    public int Score;

    [SerializeField]
    public int MaxScore;
}
