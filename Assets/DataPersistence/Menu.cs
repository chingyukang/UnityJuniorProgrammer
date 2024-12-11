using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private Text scoreText;
    [SerializeField] private Button startBtn;
    string scorePrefix = "Best Score :";

    private void Start() {
        PlayerInfo.LoadBestInfo();
        UpdateScore();
    }

    private void UpdateScore() {
        if(string.IsNullOrEmpty(PlayerInfo.BestPlayer) || PlayerInfo.BestScore == 0) {
            scoreText.text = $"{scorePrefix} ---";
        } else {
            scoreText.text = $"{scorePrefix} {PlayerInfo.BestPlayer}:{PlayerInfo.BestScore}";
        }
    }

    public void OnNameChanged(string p_name) {
        if(!string.IsNullOrEmpty(p_name)) {
            startBtn.interactable = true;
        } else {
            startBtn.interactable = false;
        }
    }

    public void OnClickStart() {
        if(nameInput.text == "RESET") {
            PlayerInfo.ResetBestInfo();
            nameInput.text = "";
            UpdateScore();
        } else {
            PlayerInfo.SetPlayerName(nameInput.text);
            SceneManager.LoadScene("MainScene");
        }
    }
}
