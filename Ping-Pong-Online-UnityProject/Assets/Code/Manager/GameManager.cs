using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Essentials")]
    public int pointsToWin;

    [Header("Multiplayer")]
    public string connectionURL = "http://localhost:1337";

    private void Update()
    {
        if(isgameStarted) 
        {
            GameWinorLose();
        }      
    }
    public void GameWinorLose() 
    {
        if(Player1_Score >= pointsToWin ||  Player2_Score >= pointsToWin) 
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
}
