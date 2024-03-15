using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] GameObject _victoryImage;
    [SerializeField] GameObject _defeatImage;
    [SerializeField] Text _timeTakenText;
    [SerializeField] Text _killsText;
    [SerializeField] Text _applesText;
    [SerializeField] GameObject _panel;
    [SerializeField] Button _continueButton;

    private void Start()
    {
        _continueButton.onClick.AddListener(() => LoadNextLevel());
    }

    void LoadNextLevel()
    {
        int nextLevel = PlayerPrefs.GetInt("Level") + 1;
        PlayerPrefs.SetInt("Level", nextLevel);
        Time.timeScale = 1;
        SceneManager.LoadScene("Singplayer");
    }

    public void EndGame(bool victory)
    {
        int timeElapsed = Timer.Instance.EndTimer();

        if (victory)
            _victoryImage.SetActive(true);
        else
            _defeatImage.SetActive(true);
        _panel.SetActive(true);

        UpdateText(timeElapsed);
    }

    void UpdateText(int _timeElapsed)
    {
        _killsText.text = GameManager.Instance.kills.ToString();
        int minutesElapsed = (int)(_timeElapsed / 60);
        int secondsElapsed = (int)(_timeElapsed % 60);

        if (minutesElapsed < 1) minutesElapsed = 0;
        if (secondsElapsed < 10)
            _timeTakenText.text = "0" + minutesElapsed + ":0" + secondsElapsed;
        else
            _timeTakenText.text = "0" + minutesElapsed + ":" + secondsElapsed;

        _applesText.text = SaveManager.Instance._saveData._playerData._apples.ToString();
    }
}
