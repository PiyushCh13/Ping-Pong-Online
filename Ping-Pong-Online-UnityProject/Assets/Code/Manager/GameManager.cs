using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SocketIOClient;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Actions")]
    public Action ResetPositionAction;
    public Action StartGameAction;
    public Action<ScoreType> UpdatePlayerScore;
    public Action PlayerWin;
    public Action PlayerLose;

    [Header("Ball")]
    public bool isBallMoving;

    [Header("Player")]
    public int Player1_Score;
    public int Player2_Score;
    public bool isgameStarted;

    [Header("Scenes")]
    public string WinorLoseScene;
    public string MultiplayerScene = "Multiplayer";

    [Header("Essentials")]
    public int pointsToWin;

    [Header("Multiplayer")]
    public string connectionURL = "http://localhost:1337";
    [HideInInspector] public string JWT_Token;
    
    [Header("Savedata")]
    [HideInInspector] public bool alreadyRegistered = true;

    [Header("InMatch")]
    public string player1_Username;
    public string player2_Username;

    public GameObject playerTemplate;

    private void Start()
    {
        SaveLoadSystem.InitialiseData();
        LoadGame();
    }
    private void Update()
    {
        if (isgameStarted)
        {
            GameWinorLose();
        }
    }
    public void GameWinorLose()
    {
        if (Player1_Score >= pointsToWin || Player2_Score >= pointsToWin)
        {
            isgameStarted = false;
            SceneManager.LoadScene(WinorLoseScene);
        }
    }

    public void ResetScore()
    {
        Player1_Score = 0;
        Player2_Score = 0;
        isBallMoving = false;
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            IS_ALREADY_REGISTERED = alreadyRegistered
        };

        string json = JsonUtility.ToJson(saveData);
        SaveLoadSystem.SaveData(json);
    }

    public void LoadGame()
    {
        string saveString = SaveLoadSystem.LoadData();

        if (saveString != null)
        {
            SaveData savedata = JsonUtility.FromJson<SaveData>(saveString);

            isgameStarted = savedata.IS_ALREADY_REGISTERED;
        }
    }

public void StartMatch(SocketIOResponse response)
{
    SceneManager.LoadScene(MultiplayerScene);

    Debug.Log("Received match data: " + response.GetValue<JObject>().ToString());

    // Parse response as JObject
    JObject data = response.GetValue<JObject>();

    // Check if "player1" and "player2" exist before accessing
    if (data.ContainsKey("player1") && data.ContainsKey("player2"))
    {
        JObject player1 = data["player1"] as JObject;
        JObject player2 = data["player2"] as JObject;

        // Ensure the values are not null
        if (player1 != null && player2 != null)
        {
            player1_Username = player1["username"]?.ToString();
            player2_Username = player2["username"]?.ToString();
        }
        else
        {
            Debug.LogError("Error: player1 or player2 is null.");
        }
    }
    else
    {
        Debug.LogError("Error: player1 or player2 key is missing in response.");
    }

    //GameObject playerTemplate = Resources.Load<GameObject>("Player_Object_Template");
    GameObject player1Object = Instantiate(playerTemplate, new Vector3(-9, 0, 0), Quaternion.identity);
    GameObject player2Object = Instantiate(playerTemplate, new Vector3(9, 0, 0), Quaternion.identity);

    player1Object.GetComponent<PlayerController>().Setup(player1_Username, true);
    player2Object.GetComponent<PlayerController>().Setup(player2_Username, false);
}
}
