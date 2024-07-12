using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CaterpillarBuilderPanel : MonoBehaviour
{
    [Header("UI Refs")]
    public GameObject _caterPillarPanelHead;
    [SerializeField] Text _leafCountText;
    [SerializeField] GameObject _extensionTypePanel;
    [SerializeField] Button _upgradeLeafButton;
    [SerializeField] Slider _progressBarSlider;
    
    public int _currentBattlepillarCost;
    private void Start()
    {
        UIManager.Instance._caterPillarBuilderPanel = this;
        _upgradeLeafButton.onClick.AddListener(() => UpgradeLeafs());
        _upgradeLeafButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = LeafManager.Instance._currentLeafLevelData._upgradeCost.ToString();
        AddSegmentsToScrollView();
        _progressBarSlider.maxValue = 1;
    }

    void UpdateProgressBar()
    {
        _progressBarSlider.value = Mathf.Lerp(_progressBarSlider.value, GameManager.Instance.GetLeafs(), Time.deltaTime * 2);
    }

    public void ResetProgressBar()
    {
        _progressBarSlider.maxValue = 1;
        _progressBarSlider.value = 0;
    }

    void UpgradeLeafs()
    {
        Debug.Log("upgrade leaf called");
        LeafManager.Instance.UpgradeLeaf();
        if (LeafManager.Instance._leafLevel == LeafManager.LeafLevels.Level5)
            _upgradeLeafButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "MAX";
        else
            _upgradeLeafButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = LeafManager.Instance._currentLeafLevelData._upgradeCost.ToString();
    }

    private void Update()
    {
        _leafCountText.text = GameManager.Instance.GetLeafs().ToString() + " / " + LeafManager.Instance._currentLeafLevelData._maxLeafs.ToString();
        if(_currentBattlepillarCost > 0)
            UpdateProgressBar();
    }

    public void AddExtension(GameManager.SegmentType type, int level)
    {
        GameObject ext = Instantiate(_caterPillarPanelHead, _caterPillarPanelHead.transform.parent);
        Sprite img = GameManager.Instance._caterPillars[GameManager.Instance._caterPillarType].GetCaterpillarExtension(type, level)._img;
        ext.GetComponent<Image>().sprite = img;
        _progressBarSlider.maxValue = _currentBattlepillarCost;
    }

    void AddSegmentsToScrollView()
    {
        Transform content = GetComponentInChildren<ScrollRect>().content.transform;
        string[] segmentinfo = PlayerPrefs.GetString("SegmentInfo").Split(":");
        foreach (string id in segmentinfo)
        {
            GameManager.SegmentType segment = (GameManager.SegmentType)Enum.ToObject(typeof(GameManager.SegmentType), Convert.ToInt32(id));
            Debug.Log(segment.ToString());
            GameObject item = Instantiate(_extensionTypePanel, content);
            item.GetComponent<CaterpillarBuilderTypePanel>()._type = segment;
            item.GetComponent<Image>().sprite = GameManager.Instance._caterPillars[0].GetCaterpillarExtension(segment, SaveManager.Instance._saveData.OwnedSegmentLevel(segment))._icon;
        }
    }
}
