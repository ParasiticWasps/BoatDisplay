using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SelectModeGUI : MonoBehaviour
{
    [SerializeField]
    private string practiceScene;

    [SerializeField]
    private string assessmentScene;

    public void OnClickedParcticeButton()
    {
        SceneManager.LoadSceneAsync(practiceScene);
    }

    public void OnClickedAssessmentButton()
    {
        SceneManager.LoadSceneAsync(assessmentScene);
    }
}
