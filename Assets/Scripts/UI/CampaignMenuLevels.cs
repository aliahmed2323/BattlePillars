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
    [SerializeField] Image _flowerImage;
    [SerializeField] Sprite[] _flowers;
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

    public void SetFlower(int score)
    {
        switch(score)
        {
            case 0:
                _flowerImage.sprite = _flowers[0];
                break;
            case 1:
                _flowerImage.sprite = _flowers[1];
                break;
            case 2:
                _flowerImage.sprite = _flowers[2];
                break;
        }

        _flowerImage.gameObject.SetActive(true);
    }
}
