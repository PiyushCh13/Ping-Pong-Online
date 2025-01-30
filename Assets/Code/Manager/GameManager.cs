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

    [Header("Scenes")]
    public string WinorLoseScene;

    private void Update()
    {
        GameWinorLose();
    }
    public void GameWinorLose() 
    {
        if(Player1_Score >= 15 ||  Player2_Score >= 15) 
        {
            SceneManager.LoadScene(WinorLoseScene, LoadSceneMode.Additive);
        }
    }

    public void ResetScore() 
    {
        Player1_Score = 0;
        Player2_Score = 0;
        isBallMoving = false;
    }
}
