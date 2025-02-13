using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampiagnLevelPopup : MonoBehaviour
{
    [SerializeField] Text _campaignLevelText;
    [SerializeField] Text _bestTimeText;
    [SerializeField] Text _rewardText;
    [SerializeField] Button _playButton;

    int _level;

    int _reward;

    private void Start()
    {
        _playButton.onClick.AddListener(() => OnPlayButtonClick());
    }
    public void UpdatePopup(string mapname, int level, int reward)
    {
        _campaignLevelText.text = mapname + " - Level " + level.ToString();
        _rewardText.text = reward.ToString();
        _bestTimeText.text = "00:00";
        _level = level;
        _reward = reward;
    }

    void OnPlayButtonClick()
    {
        PlayerPrefs.SetInt("Level", _level);
        SaveManager.Instance.AddApples(_reward);
        UIManager.Instance._segmentSelectionPanel.gameObject.SetActive(true);
    }
}
