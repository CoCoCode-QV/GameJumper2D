using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverDialog : Dialog
{

    public Text BestScoreText;
    bool m_ReplayBtnClick;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (BestScoreText)
            BestScoreText.text = SaveData.BestScore.ToString();
    }

    public void replay()
    {
        m_ReplayBtnClick = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToHome()
    {
        GameGuiManager.Ins.showGameGui(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (m_ReplayBtnClick)
        {
            GameGuiManager.Ins.showGameGui(true);
           
            GameManager.Ins.PlayerGame();
        }
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
