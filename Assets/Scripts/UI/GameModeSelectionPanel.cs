using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModeSelectionPanel : MonoBehaviour
{
    [SerializeField] Button _campaignButton;
    void Start()
    {
        UIManager.Instance._gamemodeSelectionPanel = this;
        _campaignButton.onClick.AddListener(() =>LoadSinglePlayer());
    }

    void LoadSinglePlayer()
    {
        SceneManager.LoadScene("Singplayer");
    }
}
