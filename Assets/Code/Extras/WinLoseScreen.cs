using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScreen : MonoBehaviour
{
    public string MainMenuScreen;
    public TMP_Text scoreText; 
    void Start()
    {
        scoreText.text = GameManager.Instance.Player1_Score.ToString() + "-" + GameManager.Instance.Player2_Score.ToString();
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene(MainMenuScreen);
    }
}
