using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGuiManager : Singleton<GameGuiManager>
{
    public GameObject HomeGui;
    public GameObject GameGui;
    public Text ScoreCountingText;
    public Image powerBarSlider;

    public Dialog AchievementDialog;
    public Dialog HelpDialog;
    public Dialog GameOverDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void showGameGui(bool isShow)
    {
        if (GameGui)
            GameGui.SetActive(isShow);
        if (HomeGui)
            HomeGui.SetActive(!isShow);
    }
    public void UpdateScoreCounting(int Score)
    {
        if (ScoreCountingText)
            ScoreCountingText.text = Score.ToString();

    }
    public void UpdatePowerBar(float curValue, float totalvalue)
    {
        if (powerBarSlider)
            powerBarSlider.fillAmount = curValue / totalvalue;

    }
    public void ShowAchievementDialog()
    {
        if (AchievementDialog)
            AchievementDialog.Show(true);
    }
    public void ShowHelpDialog()
    {
        if (HelpDialog)
           HelpDialog.Show(true);
    }
    public void ShowGameOverDialog()
    {
        if (GameOverDialog)
            GameOverDialog.Show(true);
    }

}
