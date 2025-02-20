using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

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


    private void Start()
    {
        //OpenRegisterPanel();
        OpenLoginPanel();

    }

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

    public void Register()
    {
        StartCoroutine(SendRegisterRequest(register_Username.text, register_Email.text, register_Password.text));
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
            Debug.Log("User Registered Successfully: " + request.downloadHandler.text);
            var res = Encoding.UTF8.GetString(request.downloadHandler.data);
            var resObj = JObject.Parse(res);
            JWT_Token = resObj["jwt"].ToString();
        }
        else
        {
            Debug.LogError("Registration Failed: " + request.error);
        }
    }

    public void Login()
    {
        StartCoroutine(SendLoginRequest(login_Email.text, login_Password.text));
    }

    private IEnumerator SendLoginRequest(string username, string password)
    {

        string jsonData = JsonConvert.SerializeObject(new Dictionary<string, string>
        {
            {"identifier", username},
            {"password", password}
        });

        Debug.Log("JSON Sent: " + jsonData);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        var request = new UnityWebRequest(GameManager.Instance.connectionURL + "/api/auth/local", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User Logged in Successfully: " + request.downloadHandler.text);
            var res = Encoding.UTF8.GetString(request.downloadHandler.data);
            var resObj = JObject.Parse(res);
            JWT_Token = resObj["jwt"].ToString();
        }
        else
        {
            Debug.LogError("Login Failed: " + request.error);
            Debug.LogError("Server Response: " + request.downloadHandler.text);
        }
    }
}
