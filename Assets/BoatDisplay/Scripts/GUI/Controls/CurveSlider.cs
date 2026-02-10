using Enviro;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CurveSlider : MonoBehaviour
{
    [Header("曲线设置")]
    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 0, 0);
    public float curveWidth = 200f;
    public float curveHeight = 100f;

    [Header("Slider组件")]
    public Slider slider;
    public RectTransform handle;
    public RectTransform handleImage;

    [Header("时间组件")]
    public TextMeshProUGUI timeText;

    void Start()
    {
        slider.onValueChanged.AddListener(UpdateHandlePosition);
        RectTransform silderRectTransform = GetComponent<RectTransform>();
        curveWidth = silderRectTransform.rect.width;
        curveHeight = silderRectTransform.rect.height;

        // 初始化手柄位置
        UpdateHandlePosition(slider.value);
    }

    private string hours = "";
    private string minutes = "";
    void UpdateHandlePosition(float value)
    {
        // 根据value在曲线上计算位置
        float normalizedValue = Mathf.Clamp01(value);
        float x = normalizedValue * curveWidth - 145.0f;
        float y = curve.Evaluate(normalizedValue) * curveHeight;

        //float x = normalizedValue;
        //float y = curve.Evaluate(normalizedValue);

        EnviroManager.instance.Time.SetTimeOfDay(slider.value * 24f);

        hours = "";
        minutes = "";
        hours += EnviroManager.instance.Time.hours.ToString().Count() < 2 ? $"0{EnviroManager.instance.Time.hours}" : $"{EnviroManager.instance.Time.hours}";
        minutes += EnviroManager.instance.Time.minutes.ToString().Count() < 2 ? $"0{EnviroManager.instance.Time.minutes}" : $"{EnviroManager.instance.Time.minutes}";

        string dataTime = $"{hours}:{minutes}";
        timeText.text = dataTime;

        // 更新手柄位置
        handle.localPosition = new Vector3(x, y, 0);
        handleImage.localPosition = new Vector3(x, handleImage.localPosition.y, 0);
    }
}