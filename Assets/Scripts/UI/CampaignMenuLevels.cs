using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CampaignMenuLevels : MonoBehaviour
{
    [SerializeField] int _level;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => LoadLevel());
    }

    void LoadLevel()
    {
        PlayerPrefs.SetInt("Level", _level);
        UIManager.Instance._segmentSelectionPanel.gameObject.SetActive(true);
    }
}
