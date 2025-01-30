using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ScoreType 
{
    Player1,
    Player2
}
public class BoundScore : MonoBehaviour
{
    public ScoreType ScoreType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) 
        {
            if(ScoreType == ScoreType.Player1) 
            {
                GameManager.Instance.Player1_Score++;
                GameManager.Instance.isBallMoving = false;
                GameManager.Instance.ResetPositionAction?.Invoke();
                GameManager.Instance.UpdatePlayerScore(ScoreType.Player1);
            }

            if (ScoreType == ScoreType.Player2)
            {
                GameManager.Instance.Player2_Score++;
                GameManager.Instance.isBallMoving = false;
                GameManager.Instance.ResetPositionAction?.Invoke();
                GameManager.Instance.UpdatePlayerScore(ScoreType.Player2);
            }
        }
    }
}
