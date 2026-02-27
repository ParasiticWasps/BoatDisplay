using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using BoatDisplay;

public class SelectModeGUI : MonoBehaviour
{
    [SerializeField]
    private string practiceScene;

    [SerializeField]
    private string assessmentScene;

    public void OnClickedParcticeButton()
    {
        GlobalVar.Mode = GameMode.Parctice;
        SceneManager.LoadSceneAsync(practiceScene);
    }

    public void OnClickedAssessmentButton() 
    {
        GlobalVar.Mode = GameMode.Assessment;
        SceneManager.LoadSceneAsync(assessmentScene);
    }
}
