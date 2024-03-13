using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CampaignMenuLevels : MonoBehaviour
{
    [SerializeField] int _level;
    [SerializeField] string _mapName;
    [SerializeField] int _reward;
    public bool _isLocked;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => LoadLevel());
    }

    void LoadLevel()
    {
        if (_isLocked)
            return;

        GetComponentInParent<CampaignScreen>()._selectedLevel = gameObject;
        GetComponentInParent<CampaignScreen>().EnablePopup(_mapName, _level, _reward);
        /*PlayerPrefs.SetInt("Level", _level);
        UIManager.Instance._segmentSelectionPanel.gameObject.SetActive(true);*/
    }
}
