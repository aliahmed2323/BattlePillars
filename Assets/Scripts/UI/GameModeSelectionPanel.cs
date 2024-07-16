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
        _campaignButton.onClick.AddListener(() => LoadCampaign());
        _endlessButton.onClick.AddListener(() =>Invoke("LoadEndless",0.2f));
    }

    void LoadEndless()
    {
        UIManager.Instance._segmentSelectionPanel._isEndless = true;
        UIManager.Instance._segmentSelectionPanel.gameObject.SetActive(true);
    }

    void LoadCampaign()
    {
        UIManager.Instance._segmentSelectionPanel._isEndless = false;
        UIManager.Instance._campaignScreen.gameObject.SetActive(true);
    }
}
