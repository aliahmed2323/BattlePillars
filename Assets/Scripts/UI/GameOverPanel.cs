using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] GameObject _victoryImage;
    [SerializeField] GameObject _defeatImage;
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
        _panel.SetActive(true);

        if (victory)
            _victoryImage.SetActive(true);
        else
            _defeatImage.SetActive(true);
    }
}
