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
    [SerializeField] List<Sprite> _icons = new();
    public int[] _cost;
    int level;
    public bool _purchased;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => HighlightSegment());
        RefreshPurchaseStatus();

        level = SaveManager.Instance._saveData.OwnedBaseUpgradeLevel(_type);
        GetComponent<Image>().sprite = _icons[level];

        _appleText.text = _cost[0].ToString();
    }

    public void RefreshPurchaseStatus()
    {
        if (SaveManager.Instance._saveData.IsBaseUpgradeOwned(_type))
        {
            _purchased = true;
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            level = SaveManager.Instance._saveData.OwnedBaseUpgradeLevel(_type);
            _appleText.text = _cost[level].ToString();
            GetComponent<Image>().sprite = _icons[level];
        }
    }

    void HighlightSegment()
    {
        GetComponentInParent<SegmentUpgradeScreen>()._textbox.GetComponent<Text>().text = _Info;

        if (_purchased)
        {
            if (level >= 2)
                return;
            GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgradeLevel = level + 1;
            GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgrade = _type;
            GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgradeCost = _cost[level + 1];
            GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgradeGameObject = gameObject;
            GetComponentInParent<SegmentUpgradeScreen>().PurchaseConfirmationPopup("Upgrade Base Attatchment");
            return;
        }

        GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgrade = _type;
        GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgradeCost = _cost[0];
        GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgradeGameObject = gameObject;
        GetComponentInParent<SegmentUpgradeScreen>()._selectedBaseUpgradeLevel = 0;
        GetComponentInParent<SegmentUpgradeScreen>().PurchaseConfirmationPopup("Purchase Base Attatchment");
    }
}
