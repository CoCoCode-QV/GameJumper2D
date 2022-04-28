
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData 
{
    public static int BestScore
    {
        set{
            if (PlayerPrefs.GetInt(PrefConsts.Best_Score, 0) < value)
            {
                PlayerPrefs.SetInt(PrefConsts.Best_Score, value);
            }
        }
        get => PlayerPrefs.GetInt(PrefConsts.Best_Score, 0);
    }
}
