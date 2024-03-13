using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampiagnLevelPopup : MonoBehaviour
{
    [SerializeField] Text _campaignLevelText;
    [SerializeField] Text _bestTimeText;
    [SerializeField] Text _rewardText;

    public void UpdatePopup(string mapname, int level, int reward)
    {
        _campaignLevelText.text = mapname + " - Level " + level.ToString();
        _rewardText.text = reward.ToString();
        _bestTimeText.text = "00:00";
    }
}
