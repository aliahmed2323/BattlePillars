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
    [SerializeField] Button _purchasePowerupScreenButton;
    [SerializeField] GameObject _SegmentScreen;
    [SerializeField] GameObject _BaseUpgradesScreen;
    [SerializeField] GameObject _PowerupScreen;

    // Purchase Segment Screen stuff
    [SerializeField] Button _segmentPurchaseButton;
    [HideInInspector]
    public GameObject _selectedSegmentGameObject;
    [HideInInspector]
    public GameManager.SegmentType _selectedSegment;
    [HideInInspector]
    public int _selectedSegmentCost;
    [HideInInspector]
    public int _selectedSegmentUpgradeLevel;
    [SerializeField] Text _appleText;
    [SerializeField] internal GameObject _purchaseConfirmPopup;
    [SerializeField] Button _purchaseConfirmButton;
    [SerializeField] Text _purchaseConfirmText;

    // Base Upgrade Screen Stuff
    [SerializeField] Button _basePurchaseButton;
    [HideInInspector]
    public GameObject _selectedBaseUpgradeGameObject;
    [HideInInspector]
    public GameManager.BaseUpgrades _selectedBaseUpgrade;
    [HideInInspector]
    public int _selectedBaseUpgradeLevel;
    [HideInInspector]
    public int _selectedBaseUpgradeCost;

    // Purchase Powerup
    [SerializeField] Button _powerupPurchaseButton;
    [HideInInspector]
    public GameObject _selectedPowerUpGameObject;
    [HideInInspector]
    public GameManager.Powerup _selectedPowerUp;
    [HideInInspector]
    public int _selectedPowerUpCost;


    private void Start()
    {
        _segmentPurchaseButton.onClick.AddListener(() => _purchaseConfirmPopup.SetActive(true));
        _purchaseConfirmButton.onClick.AddListener(() => PurchaseConfirm());
        _basePurchaseButton.onClick.AddListener(() => _purchaseConfirmPopup.SetActive(true));
        _purchaseSegmentScreenButton.onClick.AddListener(() => ChangeScreen(0));
        _purchaseBaseUpgradesScreenButton.onClick.AddListener(() => ChangeScreen(1));
        _purchasePowerupScreenButton.onClick.AddListener(() => ChangeScreen(2));
    }


    public void PurchaseConfirmationPopup(string text)
    {
        _purchaseConfirmPopup.SetActive(true);
        _purchaseConfirmText.text = text;
    }

    public void PurchaseConfirm()
    {
        if (_SegmentScreen.activeSelf)
            PurchaseSegment();
        if (_BaseUpgradesScreen.activeSelf)
            PurchaseBaseUpgrade();
        if (_PowerupScreen.activeSelf)
            PurchasePowerup();
    }

    private void Update()
    {
        _appleText.text = SaveManager.Instance.GetApples().ToString();
    }

    void ChangeScreen(int type)
    {
        switch(type)
        {
            case 0:
                _SegmentScreen.SetActive(true);
                _BaseUpgradesScreen.SetActive(false);
                _PowerupScreen.SetActive(false);
                break;
            case 1:
                _SegmentScreen.SetActive(false);
                _BaseUpgradesScreen.SetActive(true);
                _PowerupScreen.SetActive(false);
                break;
            case 2:
                _SegmentScreen.SetActive(false);
                _BaseUpgradesScreen.SetActive(false);
                _PowerupScreen.SetActive(true);
                break;
        }
    }

    void PurchaseSegment()
    {
        if(SaveManager.Instance.DeductApples(_selectedSegmentCost))
        {
            SavesScriptableObject.OwnedSegments l;
            l._segmentType = _selectedSegment;
            l._segmentLevel = _selectedSegmentUpgradeLevel;

            SaveManager.Instance._saveData.RemoveSegment(_selectedSegment);

            SaveManager.Instance._saveData._playerData._ownedSegments.Add(l);
            _selectedSegmentGameObject.GetComponent<SegmentUpgradeButton>().RefreshPurchaseStatus();
        }
        _purchaseConfirmPopup.SetActive(false);
    }

    void PurchaseBaseUpgrade()
    {
        if (SaveManager.Instance.DeductApples(_selectedBaseUpgradeCost))
        {
            SavesScriptableObject.OwnedBaseUpgrades b = new();
            b.BaseUpgrades = _selectedBaseUpgrade;
            b.Level = _selectedBaseUpgradeLevel;

            SaveManager.Instance._saveData.RemoveBaseUpgrade(_selectedBaseUpgrade);

            SaveManager.Instance._saveData._playerData._baseUpgrades.Add(b);
            _selectedBaseUpgradeGameObject.GetComponent<BaseUpgradeButton>().RefreshPurchaseStatus();
        }
        _purchaseConfirmPopup.SetActive(false);
    }

    void PurchasePowerup()
    {
        if (SaveManager.Instance.DeductApples(_selectedPowerUpCost))
        {
            SaveManager.Instance._saveData.AddPowerup(_selectedPowerUp);
            _selectedPowerUpGameObject.GetComponent<PowerupButton>().RefreshPurchaseStatus();
        }
        _purchaseConfirmPopup.SetActive(false);
    }
}
