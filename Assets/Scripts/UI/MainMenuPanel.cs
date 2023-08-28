using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] Button _playButton;

    private void Start()
    {
        _playButton.onClick.AddListener(() => OpenMainMenuPanel());
        UIManager.Instance._mainMenuPanel = this;
    }

    void OpenMainMenuPanel()
    {
        UIManager.Instance._gamemodeSelectionPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
