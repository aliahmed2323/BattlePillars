using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSelectionPanel : MonoBehaviour
{
    [SerializeField] Button _campaignButton;
    [SerializeField] Button _endlessButton;
    void Start()
    {
        UIManager.Instance._gamemodeSelectionPanel = this;
        _endlessButton.onClick.AddListener(() =>Invoke("LoadEndless",0.2f));
    }

    void LoadEndless()
    {
        UIManager.Instance._segmentSelectionPanel.gameObject.SetActive(true);
    }
}
