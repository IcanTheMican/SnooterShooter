using System;
using UnityEngine;

[Serializable]
public static class SaveSystem
{
    private const string HIGH_SCORE = "HIGH_SCORE";

    public static void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, highScore);
        PlayerPrefs.Save();
    }

    public static int LoadHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE, 0);
    }
}