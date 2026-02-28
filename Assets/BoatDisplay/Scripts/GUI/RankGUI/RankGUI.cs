using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankGUI : MonoBehaviour
{
    [SerializeField] private RandItem itemPerfab;

    [SerializeField] private Transform content;

    private List<RandItem> m_itemBuffer = new List<RandItem>();

    private void Start()
    {
        //Setup();
        ResetGUI();
    }

    public void ResetGUI()
    {
        foreach (var item in m_itemBuffer)
        {
            item.gameObject.SetActive(false);
        }
        m_itemBuffer.Clear();

        List<UserData> list = new List<UserData>();
        for (int i = 0; i < DataBase.Get().UsrList.Count; i++)
        {
            list.Add(DataBase.Get().UsrList[i]);
        }
        list.Sort((a, b) => b.Score.CompareTo(a.Score));

        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(itemPerfab.gameObject, content);
            RandItem item = go.GetComponent<RandItem>();
            m_itemBuffer.Add(item);
            item.Setup((i + 1).ToString(), list[i].Account, list[i].Score.ToString());
        }
    }
}
