using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginGUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField accountInput;

    [SerializeField] private TMP_InputField pwdInput;

    [SerializeField] private Button loginButton;

    private void Start()
    {
        
    }

    public void OnClickedLoginButton()
    {
        string account = accountInput.text;
        string pwd = pwdInput.text;
        LoginLogic.Get().LoginRequest(account, pwd, LoginSuccessed, LoginFailed);
    }

    private void LoginSuccessed()
    {

    }

    private void LoginFailed()
    {

    }
}
