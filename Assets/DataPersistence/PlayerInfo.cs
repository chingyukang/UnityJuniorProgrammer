using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo
{
    public static string BestPlayer { get; private set; } = "";
    public static int BestScore { get; private set; } = 0;

    public static string CurrentName { get; private set; } = "";
    public static int CurrentScore { get; private set; } = 0;

    public static void LoadPlayerInfo() {

    }

    public static void SavePlayerInfo() {

    }

    public static void SetPlayerName(string p_name) {
        CurrentName = p_name;
        CurrentScore = 0;
    }

    public static void AddScore(int p_score) {
        CurrentScore += p_score;

        if(CurrentScore > BestScore) {
            BestPlayer = CurrentName;
            BestScore = CurrentScore;
        }
    }
}
