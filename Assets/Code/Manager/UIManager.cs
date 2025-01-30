using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text player1Text;
    public TMP_Text player2Text;

    private void Start()
    {
        GameManager.Instance.UpdatePlayerScore = UpdateScore;
    }

    public void UpdateScore(ScoreType scoreType)
    {
        if(ScoreType.Player1 == scoreType) 
        {
            player1Text.text = GameManager.Instance.Player1_Score.ToString();
        }

        if (ScoreType.Player2 == scoreType)
        {
            player2Text.text = GameManager.Instance.Player2_Score.ToString();
        }
    }
}
