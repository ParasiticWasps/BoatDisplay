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
}
