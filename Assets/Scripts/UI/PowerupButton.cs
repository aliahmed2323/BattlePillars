using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupButton : MonoBehaviour
{
     string _Info;
    [SerializeField] GameManager.Powerup _type;
    [SerializeField] Text _appleText;
    [SerializeField] Text _ownedAmount;
    public int _cost;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => HighlightSegment());
        RefreshPurchaseStatus();

        SetInfo(_type);

        _appleText.text = _cost.ToString();
        _ownedAmount.text = SaveManager.Instance._saveData.GetOwnedPowerup(_type).ToString();
    }

    public void RefreshPurchaseStatus()
    {
        _ownedAmount.text = SaveManager.Instance._saveData.GetOwnedPowerup(_type).ToString();
        SetInfo(_type);
        GetComponentInParent<SegmentUpgradeScreen>()._textbox.GetComponent<Text>().text = _Info;
    }

    string PowerupText(GameManager.Powerup powerup)
    {
        switch(powerup)
        {
            case GameManager.Powerup.MrLeaf:
                return "Mr.Leaf";
            case GameManager.Powerup.LarryTheBird:
                return "Larry the Bird";
            case GameManager.Powerup.WizardTurtle:
                return "Wizard Turtle";
            default:
                return "nill";
        }
    }

    void SetInfo(GameManager.Powerup type)
    {
        switch(type)
        {
            case GameManager.Powerup.MrLeaf:
                _Info = $"MR.LEAF: {_ownedAmount.text}/99 OWNED \n This helpful fellow brings you a pile of leaves... But at what cost?";
                break;
            case GameManager.Powerup.LarryTheBird:
                _Info = $"Larry the Bird: {_ownedAmount.text}/99 OWNED \n Unleashes a deluge of doody from it's irritable bowels to smite your foes";
                break;
            case GameManager.Powerup.WizardTurtle:
                _Info = $"Wizard Turtle: {_ownedAmount.text}/99 OWNED \n The Wizard Turtle curses your enemies with turtle speed. Hooray!";
                break;
        }
    }

    void HighlightSegment()
    {
        GetComponentInParent<SegmentUpgradeScreen>()._textbox.GetComponent<Text>().text = _Info;

        GetComponentInParent<SegmentUpgradeScreen>()._selectedPowerUp = _type;
        GetComponentInParent<SegmentUpgradeScreen>()._selectedPowerUpCost = _cost;
        GetComponentInParent<SegmentUpgradeScreen>()._selectedPowerUpGameObject = gameObject;
        GetComponentInParent<SegmentUpgradeScreen>().PurchaseConfirmationPopup($"Purchase {PowerupText(_type)}");
    }
}
