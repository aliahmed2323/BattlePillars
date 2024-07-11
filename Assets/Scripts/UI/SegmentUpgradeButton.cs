using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentUpgradeButton : MonoBehaviour
{
    [TextArea]
    [SerializeField] string _segmentInfo;
    [SerializeField] GameManager.SegmentType _segmentType;
    [SerializeField] Text _appleText;
    int level;
    public int _cost;
    public bool _purchased;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => HighlightSegment());
        RefreshPurchaseStatus();

        level = SaveManager.Instance._saveData.OwnedSegmentLevel(_segmentType);
        GetComponent<Image>().sprite = GameManager.Instance._caterPillars[0].GetCaterpillarExtension(_segmentType, level)._img;


        if (!_purchased)
            _appleText.text = _cost.ToString();
        else
        {
            _cost = GameManager.Instance._caterPillars[0].GetCaterpillarExtension(_segmentType, level)._upgradeCost;
            _appleText.text = _cost.ToString();
        }

    }

    public void RefreshPurchaseStatus()
    {
        if (SaveManager.Instance._saveData.IsSegmentOwned(_segmentType))
        {
            _purchased = true;
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    void HighlightSegment()
    {
        GetComponentInParent<SegmentUpgradeScreen>()._textbox.GetComponent<Text>().text = _segmentInfo;

        if (_purchased)
        {
            GetComponentInParent<SegmentUpgradeScreen>()._selectedSegmentUpgradeLevel = level + 1;
            GetComponentInParent<SegmentUpgradeScreen>()._selectedSegment = _segmentType;
            GetComponentInParent<SegmentUpgradeScreen>()._selectedSegmentCost = _cost;
            GetComponentInParent<SegmentUpgradeScreen>()._selectedSegmentGameObject = gameObject;
            return;
        }

        GetComponentInParent<SegmentUpgradeScreen>()._selectedSegment = _segmentType;
        GetComponentInParent<SegmentUpgradeScreen>()._selectedSegmentCost = _cost;
        GetComponentInParent<SegmentUpgradeScreen>()._selectedSegmentGameObject = gameObject;
        GetComponentInParent<SegmentUpgradeScreen>()._selectedSegmentUpgradeLevel = 1;
    }
}
