using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentUpgradeScreen : MonoBehaviour
{
    // general stuff
    public GameObject _textbox;
    [SerializeField] Button _purchaseSegmentScreenButton;
    [SerializeField] Button _purchaseBaseUpgradesScreenButton;
    [SerializeField] GameObject _SegmentScreen;
    [SerializeField] GameObject _BaseUpgradesScreen;

    // Purchase Segment Screen stuff
    [SerializeField] Button _segmentPurchaseButton;
    [HideInInspector]
    public GameObject _selectedSegmentGameObject;
    [HideInInspector]
    public GameManager.SegmentType _selectedSegment;
    [HideInInspector]
    public int _selectedSegmentCost;
    [SerializeField] Text _appleText;
    [SerializeField] GameObject _purchaseConfirmPopup;
    [SerializeField] Button _purchaseConfirmButton;

    // Base Upgrade Screen Stuff
    [SerializeField] Button _basePurchaseButton;
    [HideInInspector]
    public GameObject _selectedBaseUpgradeGameObject;
    [HideInInspector]
    public GameManager.BaseUpgrades _selectedBaseUpgrade;
    [HideInInspector]
    public int _selectedBaseUpgradeCost;


    private void Start()
    {
        _segmentPurchaseButton.onClick.AddListener(() => _purchaseConfirmPopup.SetActive(true));
        _purchaseConfirmButton.onClick.AddListener(() => PurchaseConfirm());
        _basePurchaseButton.onClick.AddListener(() => _purchaseConfirmPopup.SetActive(true));
        _purchaseSegmentScreenButton.onClick.AddListener(() => ChangeScreen(true));
        _purchaseBaseUpgradesScreenButton.onClick.AddListener(() => ChangeScreen(false));
    }


    void PurchaseConfirm()
    {
        if (_SegmentScreen.activeSelf)
            PurchaseSegment();
        if (_BaseUpgradesScreen.activeSelf)
            PurchaseBaseUpgrade();
    }

    private void Update()
    {
        _appleText.text = SaveManager.Instance.GetApples().ToString();
    }

    void ChangeScreen(bool segmentSelection)
    {
        if(segmentSelection)
        {
            _SegmentScreen.SetActive(true);
            _BaseUpgradesScreen.SetActive(false);
            return;
        }

        _SegmentScreen.SetActive(false);
        _BaseUpgradesScreen.SetActive(true);
    }

    void PurchaseSegment()
    {
        if(SaveManager.Instance.DeductApples(_selectedSegmentCost))
        {
            SavesScriptableObject.OwnedSegments l;
            l._segmentType = _selectedSegment;
            l._segmentLevel = 1;
            SaveManager.Instance._saveData._playerData._ownedSegments.Add(l);
            _selectedSegmentGameObject.GetComponent<SegmentUpgradeButton>().RefreshPurchaseStatus();
        }
        _purchaseConfirmPopup.SetActive(false);
    }

    void PurchaseBaseUpgrade()
    {
        if (SaveManager.Instance.DeductApples(_selectedSegmentCost))
        {
            SaveManager.Instance._saveData._playerData._baseUpgrades.Add(_selectedBaseUpgrade);
            _selectedBaseUpgradeGameObject.GetComponent<BaseUpgradeButton>().RefreshPurchaseStatus();
        }
        _purchaseConfirmPopup.SetActive(false);
    }
}
