using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplayerUIManager : MonoBehaviour
{

    public TMP_Text player1username;
    public TMP_Text player2username;
    public TMP_Text player1scoreText;
    public TMP_Text player2scoreText;

    private void Start()
    {
        player1username.text = GameManager.Instance.player1_Username;
        player2username.text = GameManager.Instance.player2_Username;
    }

    public void UpdateScore(ScoreType scoreType)
    {
        if(ScoreType.Player1 == scoreType) 
        {
            player1scoreText.text = GameManager.Instance.Player1_Score.ToString();
        }

        if (ScoreType.Player2 == scoreType)
        {
            player2scoreText.text = GameManager.Instance.Player2_Score.ToString();
        }
    }
}
