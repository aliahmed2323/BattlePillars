using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button _exitButton;
    [SerializeField] Button _resumeButton;

    private void Start()
    {
        _resumeButton.onClick.AddListener(() => gameObject.SetActive(false));
        _exitButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
