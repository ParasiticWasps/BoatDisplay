using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum HVType
{
    Vertical,
    Horizontal
}
public class LoopScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public HVType tempHVType;

    [Header("中心点")]
    public Transform Centre;

    [Header("UI父级")]
    public Transform Content;

    [Header("间隔距离")]
    public float SpacingDistance = 100;

    [Header("缩放倍数")]
    public float Scale = 1;

    [Header("居中吸附速度")]
    public float Speed = 10;

    //[Header("当前居中物体名称")]
    //public Text tempCentreName;

    //[Header("左右切换按钮")]
    //public Button Left, Right;

    [Header("居中后UI缩放系数")]
    public Vector3 CentreScale = Vector3.one * 1.5f;

    public List<Transform> ItemList;

    [HideInInspector] public Transform TempCentre; //当前居中物体

    bool isDrag = false, isAdsorption, isScale, isLRBtn;//拖拽更换当前索引，控制吸附居中,控制缩放，控制左右按钮

    float MaxDis, MinDis;//最大最小距离

    private Vector3 startPos, endPos, BtnPos;//左边限制位置，右边限制位置 

    private static LoopScroll instance;

    public static LoopScroll Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<LoopScroll>();
        }
        return instance;
    }

    #region 系统函数或默认接口实现

    private void Awake() { }

    void Start()
    {
        ApplyEqualSpacing();//根据默认第一个位置根据设置刷新一遍坐标

        isScale = true;
        isAdsorption = true;
        TempCentre = ItemList[0];
        //if (tempCentreName) { tempCentreName.text = TempCentre.name; }

        if (tempHVType == HVType.Horizontal)
        {
            startPos = ItemList[0].position;
            startPos.x -= ItemList[0].GetComponent<RectTransform>().rect.width;

            endPos = ItemList[ItemList.Count - 1].position;
            endPos.x += ItemList[ItemList.Count - 1].GetComponent<RectTransform>().rect.width;
        }
        else
        {
            startPos = ItemList[0].position;
            startPos.y += ItemList[0].GetComponent<RectTransform>().rect.height;

            endPos = ItemList[ItemList.Count - 1].position;
            endPos.y -= ItemList[ItemList.Count - 1].GetComponent<RectTransform>().rect.height;
        }

        //设置第一个坐标与最后一个坐标位置  
        //求出最远距离
        MinDis = Vector3.Distance(ItemList[0].position, Centre.position);
        for (int i = 0; i < ItemList.Count; i++)
        {
            var dis = Vector3.Distance(ItemList[i].position, Centre.position);
            if (dis > MaxDis)
            {
                MaxDis = dis;
            }
            else if (dis < MinDis) { MinDis = dis; }
        }

        //if (Left)
        //{
        //    Left.onClick.AddListener(LBtn);
        //    Right.onClick.AddListener(RBtn);
        //}

        isDrag = true;
    }

    void Update()
    {
        //Islimit();

        Adsorption();

        ScaleDistance();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isAdsorption = false;
        isScale = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<ScrollRect>().velocity = Vector3.one * 0.5f;
        FindMinDis();
        isAdsorption = true;
        isScale = true;
        isDrag = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //isDrag = true;
    }

    #endregion

    float current = 0, current2;//当前最后一位的坐标
    void ApplyEqualSpacing()
    {
        // 计算总宽度
        float totalWidth = 0f;
        foreach (RectTransform element in ItemList)
        {
            if (tempHVType == HVType.Horizontal)
                totalWidth += element.sizeDelta.x;
            else
            {
                totalWidth += element.sizeDelta.y;
            }
        }

        // 计算间隔的总宽度
        float totalSpacing = (ItemList.Count - 1) * SpacingDistance;

        // 计算每个元素的平均宽度
        float averageWidth = (totalWidth + totalSpacing) / ItemList.Count;

        // 设置每个元素的位置
        if (tempHVType == HVType.Horizontal)
        {
            current = ItemList[0].GetComponent<RectTransform>().anchoredPosition.x;
            current2 = ItemList[0].GetComponent<RectTransform>().anchoredPosition.x;
        }
        else
        {
            current = ItemList[0].GetComponent<RectTransform>().anchoredPosition.y;
            current2 = ItemList[0].GetComponent<RectTransform>().anchoredPosition.y;
        }

        for (int i = 0; i < ItemList.Count; i++)
        {
            RectTransform element = ItemList[i].GetComponent<RectTransform>();
            if (tempHVType == HVType.Horizontal)
            {
                float elementWidth = element.sizeDelta.x;
                // 设置元素的位置
                element.anchoredPosition = new Vector2(current + elementWidth / 2f, element.anchoredPosition.y);
                // 更新下一个元素的X坐标
                current += elementWidth + SpacingDistance;
            }
            else
            {
                float elementWidth = element.sizeDelta.y;
                // 设置元素的位置
                element.anchoredPosition = new Vector2(element.anchoredPosition.x, current + elementWidth / 2f);
                // 更新下一个元素的X坐标
                current -= elementWidth + SpacingDistance;
            }
        }
    }

    void Islimit() //设置坐标切换与列表内元素与面板层级切换 保证层级与列表内数据同步
    {
        if (isDrag)
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                var currentItem = ItemList[i];

                if (tempHVType == HVType.Horizontal)
                {
                    var elementWidth = currentItem.GetComponent<RectTransform>().sizeDelta.x;
                    if (currentItem.position.x < startPos.x)
                    {
                        currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(current + elementWidth / 2, 0, 0);
                        current += elementWidth + SpacingDistance;
                        current2 += SpacingDistance + elementWidth;
                    }
                    else
                   if (currentItem.position.x > endPos.x)
                    {
                        current2 -= SpacingDistance + elementWidth;
                        current -= elementWidth + SpacingDistance;
                        currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector3((current2 + elementWidth / 2), 0, 0);
                    }
                }
                else
                {
                    var elementWidth = currentItem.GetComponent<RectTransform>().sizeDelta.y;

                    if (currentItem.position.y > startPos.y)//向下托 当前坐标比初始坐标高
                    {


                        currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(currentItem.GetComponent<RectTransform>().anchoredPosition.x, (current + elementWidth / 2), 0);

                        current -= elementWidth + SpacingDistance;
                        current2 -= SpacingDistance + elementWidth;
                    }
                    else
                    if (currentItem.position.y < endPos.y)//向下托 当前坐标比终点坐标高
                    {
                        current2 += SpacingDistance + elementWidth;
                        current += elementWidth + SpacingDistance;
                        currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(currentItem.GetComponent<RectTransform>().anchoredPosition.x, (current2 + elementWidth / 2), 0);
                    }
                }
            }
        }
    }

    void ScaleDistance()//根据百分比设置缩放动画
    {
        if (isScale)
        {
            if (TempCentre)
            {
                for (int i = 0; i < ItemList.Count; i++)
                {
                    if (TempCentre == ItemList[i])
                    {
                        var scale = Vector3.Lerp(ItemList[i].localScale, CentreScale, Speed * Time.deltaTime);
                        TempCentre.localScale = scale;
                        if (Vector3.Distance(TempCentre.localScale, CentreScale) < 0.01f)
                        {
                            isLRBtn = false;
                            isScale = false;
                        }
                    }
                    else
                    {
                        var scale = Vector3.Lerp(ItemList[i].localScale, Vector3.one, Speed * Time.deltaTime);
                        ItemList[i].localScale = scale;
                    }
                }
            }
        }
    }

    void Adsorption() //停止滑动进行吸附
    {
        if (isAdsorption)
        {
            if (TempCentre)
            {
                var dis = Centre.position - TempCentre.position;
                if (tempHVType == HVType.Horizontal)
                {
                    var pos = Vector3.Lerp(Content.position, new Vector3(Content.position.x + dis.x,
                  Content.position.y, Content.position.z), Speed * Time.deltaTime);
                    Content.position = pos;
                    if (Vector3.Distance(new Vector3(Content.position.x + dis.x,
                        Content.position.y, Content.position.z), Content.position) < 0.01f)
                    {
                        isAdsorption = false;
                    }
                }
                else
                {
                    var pos = Vector3.Lerp(Content.position, new Vector3(Content.position.x,
                    Content.position.y + dis.y, Content.position.z), Speed * Time.deltaTime);

                    Content.position = pos;

                    if (Vector3.Distance(new Vector3(Content.position.x,
                        Content.position.y + dis.y, Content.position.z), Content.position) < 0.01f)
                    {
                        isAdsorption = false;
                    }
                }
            }
        }
    }

    void FindMinDis() //找出距离中心点最近的  
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            float dis = Vector3.Distance(ItemList[i].position, Centre.position);
            if (dis < MinDis)
            {
                TempCentre = ItemList[i];
                //if (tempCentreName)
                //{
                //    tempCentreName.text = ItemList[i].name;
                //}

                MinDis = dis;
            }
        }
        MinDis = 1000;
    }

    public Transform GetCenterItem()
    {
        return TempCentre;
    }
}

