using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginManager : Singleton<LoginManager>
{
    public TMP_InputField register_Username;
    public TMP_InputField register_Password;
    public TMP_InputField register_Email;
    public TMP_InputField login_Password;
    public TMP_InputField login_Email;

    public string JWT_Token;

    public GameObject loginPanel;

    public GameObject registerPanel;

    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    public void OpenRegisterPanel()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
