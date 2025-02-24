using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : Singleton<LoginManager>
{
    public TMP_InputField register_Username;
    public TMP_InputField register_Password;
    public TMP_InputField register_Email;
    public TMP_InputField login_Password;
    public TMP_InputField login_Email;



    public GameObject loginPanel;

    public GameObject registerPanel;
    public GameObject matchmakingPanel;

    public Button matchmakingButton;


    void Start()
    {
        if (GameManager.Instance.alreadyRegistered)
        {
            OpenLoginPanel();
        }
        else
        {
            OpenRegisterPanel();
        }
    }

    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        matchmakingPanel.SetActive(false);
    }

    public void OpenRegisterPanel()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
        matchmakingPanel.SetActive(false);
    }

    public void OpenMatchmakingPanel()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        matchmakingPanel.SetActive(true);
    }

    public void Register()
    {
        StartCoroutine(SendRegisterRequest(register_Username.text, register_Email.text, register_Password.text));
        GameManager.Instance.alreadyRegistered = true;
        GameManager.Instance.SaveGame();
        OpenLoginPanel();
    }


    private IEnumerator SendRegisterRequest(string username, string email, string password)
    {

        string jsonData = JsonConvert.SerializeObject(new Dictionary<string, string>
        {
            {"username", username},
            {"email", email},
            {"password", password}
        });

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(GameManager.Instance.connectionURL + "/api/auth/local/register", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var res = Encoding.UTF8.GetString(request.downloadHandler.data);
            var resObj = JObject.Parse(res);
            GameManager.Instance.JWT_Token = resObj["jwt"].ToString();
        }
    }

    public void Login()
    {
        StartCoroutine(SendLoginRequest(login_Email.text, login_Password.text));
        OpenMatchmakingPanel();
    }

    public void StartMatchmaking()
    {
        SocketManager.Instance.StartMatchmaking();
        matchmakingButton.interactable = false;
    }

    private IEnumerator SendLoginRequest(string username, string password)
    {
        string jsonData = JsonConvert.SerializeObject(new Dictionary<string, string>
        {
            {"identifier", username},
            {"password", password}
        });

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        var request = new UnityWebRequest(GameManager.Instance.connectionURL + "/api/auth/local", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var res = Encoding.UTF8.GetString(request.downloadHandler.data);
            var resObj = JObject.Parse(res);
            //Debug.Log(resObj);
            GameManager.Instance.JWT_Token = resObj["jwt"].ToString();
            Debug.Log(GameManager.Instance.JWT_Token);
        }
        else
        {
            Debug.LogError("Login Failed! Error: " + request.error);
            Debug.LogError("Response Code: " + request.responseCode);
            Debug.LogError("Server Response: " + request.downloadHandler.text);
        }
    }
}
