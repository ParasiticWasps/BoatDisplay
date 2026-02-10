using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPanel
{
    TimePanel = 0 << 1,
    WeatherPanel = 0 << 2,
    BoatPanel = 0 << 3,
    Neno = -1
}


public class GuiManager : MonoBehaviour
{
    private static GuiManager instance;

    public static GuiManager Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<GuiManager>();
        }
        return instance;
    }    

    public List<BaseGui> guiList = new List<BaseGui>();

    public void SetPanlesActive(int bit)
    {
        for (int i = 0; i < guiList.Count; i++)
        {
            bool isTrue = ((int)guiList[i].guiName & bit) != 0;
            guiList[i].gameObject.SetActive(isTrue);
        }
    }

    public void Register(BaseGui gui)
    {
        guiList.Add(gui);
    }
}
