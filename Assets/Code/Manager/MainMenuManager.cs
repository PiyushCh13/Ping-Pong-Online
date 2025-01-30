using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string SinglePlayerScene;
    public string MultiplayerScene;

    void Awake()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }

    private void Start()
    {
        GameManager.Instance.ResetScore();
    }

    public void OpenSinglePlayer() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.collisionSound);
        SceneManager.LoadScene(SinglePlayerScene, LoadSceneMode.Additive);

    }

    public void OpenMultiPlayer()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.collisionSound);
        SceneManager.LoadScene(MultiplayerScene, LoadSceneMode.Additive);
    }

    public void Quit()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.collisionSound);
        Application.Quit();
    }
}
