using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private static DataBase instance;

    public static DataBase Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<DataBase>();
        }
        return instance;
    }

    private List<UserData> usrList = new List<UserData>();

    private string USER_DATA_PATH = Path.Combine(Application.persistentDataPath, "user.json");

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Save(UserData data)
    {
        int idx = usrList.FindIndex(_ => data.Account == _.Account);

        if (idx >= 0 && idx < usrList.Count)
        {
            usrList[idx] = data;
        }
        else
        {
            usrList.Add(data);
        }

        string jsondata = JsonMapper.ToJson(usrList);
        File.WriteAllText(USER_DATA_PATH, jsondata);
    }

    public void Load()
    {
        if (File.Exists(USER_DATA_PATH))
        {
            string jsonString = File.ReadAllText(USER_DATA_PATH);
            usrList = JsonMapper.ToObject<List<UserData>>(jsonString);
        }
    }

    public bool Exists(string account)
    {
        int idx = usrList.FindIndex(_ => account == _.Account);
        return idx >= 0 && idx < usrList.Count;
    }

    public UserData GetData(string account)
    {
        return usrList.Find(_  => account == _.Account);
    }
}
