using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Color> imgColors;

    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text yourScoreText;
    [SerializeField] private Text clickText;
    [SerializeField] private Clicker clicker;

    string bestScorePrefix = "Best Score :";
    string yourScorePrefix = "Your Score :";

    string clickTextStr = "Just Click !";
    string criticalStr = "Critical Hit +10 !";

    Coroutine hitCoroutine = null;

    private void Start() {
        clicker.OnClick += OnClick;

        if(string.IsNullOrEmpty(PlayerInfo.BestPlayer) || PlayerInfo.BestScore == 0) {
            bestScoreText.text = $"{bestScorePrefix} ---";
        } else {
            bestScoreText.text = $"{bestScorePrefix} {PlayerInfo.BestPlayer}:{PlayerInfo.BestScore}";
        }
    }

    public void OnTakeABreak() {
        SceneManager.LoadScene("Menu");
    }

    private void OnClick() {
        image.color = imgColors[Random.Range(0, imgColors.Count)];

        int _random = Random.Range(0, 100);
        if(_random < 10) {
            PlayerInfo.AddScore(10);
            clickText.text = criticalStr;
        } else {
            PlayerInfo.AddScore(1);
            clickText.text = clickTextStr;
        }

        bestScoreText.text = $"{bestScorePrefix} {PlayerInfo.BestPlayer}:{PlayerInfo.BestScore}";
        yourScoreText.text = $"{yourScorePrefix} {PlayerInfo.CurrentScore}";

        StartCoroutine(HitCoroutine());
    }

    private IEnumerator HitCoroutine() {
        clickText.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        clickText.color = Color.white;
    }

}
