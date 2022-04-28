using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementDialog : Dialog
{
    public Text BestScoreText;

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (BestScoreText)
            BestScoreText.text = SaveData.BestScore.ToString();
    }
}
