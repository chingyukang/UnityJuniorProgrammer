using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo
{
    public static string BestPlayer { get; private set; } = "";
    public static int BestScore { get; private set; } = 0;

    public static string CurrentName { get; private set; } = "";
    public static int CurrentScore { get; private set; } = 0;

    static string bestInfoPath = Application.persistentDataPath + "/bestInfo.txt";

    public static void LoadBestInfo() {
        if(!System.IO.File.Exists(bestInfoPath)) {
            System.IO.File.Create(bestInfoPath).Dispose();
        } else {
            string _info = System.IO.File.ReadAllText(bestInfoPath);
            if(!string.IsNullOrEmpty(_info)) {
                string[] _infoArr = _info.Split(':');
                BestPlayer = _infoArr[0];
                BestScore = int.Parse(_infoArr[1]);
            }
        }
    }

    public static void SaveBestInfo() {
        string _info = $"{BestPlayer}:{BestScore}";
        System.IO.File.WriteAllText(bestInfoPath, _info);
    }

    public static void ResetBestInfo() {
        BestPlayer = "";
        BestScore = 0;
        System.IO.File.WriteAllText(bestInfoPath,"");
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

            SaveBestInfo();
        }
    }
}
