using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginGUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField accountInput;

    [SerializeField] private TMP_InputField pwdInput;

    [SerializeField] private Button loginButton;

    [SerializeField] private Text hintText;

    private SelectModeGUI selectModeGUI;

    private void Awake()
    {
        selectModeGUI = FindObjectOfType<SelectModeGUI>();
        selectModeGUI.gameObject.SetActive(false);

        Debug.Log($"usrname: {PlayerInfoHandle.Get().CurrentPlayerData.Account}, score: {PlayerInfoHandle.Get().CurrentPlayerData.Score}");
        if (PlayerInfoHandle.Get().CurrentPlayerData?.Account.Count() > 0)
        {
            LoginSuccessed();
        }
    }

    private void Start()
    {
        hintText.gameObject.SetActive(false);
    }

    public void OnClickedLoginButton()
    {
        string account = accountInput.text;
        string pwd = pwdInput.text;
        LoginLogic.Get().LoginRequest(account, pwd, LoginSuccessed, LoginFailed);
    }

    private void LoginSuccessed()
    {
        if (selectModeGUI != null)
        {
            selectModeGUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void LoginFailed()
    {
        hintText.gameObject.SetActive(true);
    }
}
