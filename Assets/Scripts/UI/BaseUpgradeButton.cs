using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUpgradeButton : MonoBehaviour
{
    [TextArea]
    [SerializeField] string _Info;
    [SerializeField] GameManager.BaseUpgrades _type;
    [SerializeField] Text _appleText;
    public int _cost;
    public bool _purchased;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => HighlightSegment());
        RefreshPurchaseStatus();

        _appleText.text = _cost.ToString();
    }

    public void RefreshPurchaseStatus()
    {
        if (SaveManager.Instance._saveData.IsBaseUpgradeOwned(_type))
        {
            _purchased = true;
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    void HighlightSegment()
    {
        GetComponentInParent<SegmentUpgradeScreen>()._textbox.GetComponent<Text>().text = _Info;

        if (_purchased)
            return;

        GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgrade = _type;
        GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgradeCost = _cost;
        GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgradeGameObject = gameObject;
    }
}
