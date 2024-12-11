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
        PlayerInfo.LoadPlayerInfo();

        if(string.IsNullOrEmpty(PlayerInfo.CurrentName) || PlayerInfo.CurrentScore == 0) {
            scoreText.text = $"{scorePrefix} ---";
        } else {
            scoreText.text = $"{scorePrefix} {PlayerInfo.CurrentName}:{PlayerInfo.CurrentScore}";
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
        PlayerInfo.SetPlayerName(nameInput.text);
        SceneManager.LoadScene("MainScene");
    }
}
