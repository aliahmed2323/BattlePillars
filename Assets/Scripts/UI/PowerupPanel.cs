using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerupPanel : MonoBehaviour
{
    [SerializeField] GameObject _powerUpOptionsPanel;
    [SerializeField] Button _powerUpPanelButton;

    [SerializeField] Button _MrLeafButton;
    [SerializeField] Button _LarryButton;
    [SerializeField] Button _WizardButton;

    [SerializeField] GameObject _MrLeafAnimObject;
    [SerializeField] GameObject _WizardAnimObject;

    [SerializeField] GameObject _poopObject;


    [SerializeField] Text _ownedWizardText;
    [SerializeField] Text _ownedMrLeafText;
    [SerializeField] Text _ownedLarryText;

    bool _isOpen = false;
    bool _canOpen = true;
    private void Start()
    {
        RefreshOwnedPowerups();
        _powerUpPanelButton.onClick.AddListener(()=>PowerUpPanel(!_isOpen));
        _MrLeafButton.onClick.AddListener(()=>OnPowerUpClick(GameManager.Powerup.MrLeaf));
        _LarryButton.onClick.AddListener(()=>OnPowerUpClick(GameManager.Powerup.LarryTheBird));
        _WizardButton.onClick.AddListener(()=>OnPowerUpClick(GameManager.Powerup.WizardTurtle));
    }

    public void PowerUpPanel(bool open)
    {
        if (open)
        {
            DOTween.To(() => _powerUpOptionsPanel.GetComponent<Image>().fillAmount, x => _powerUpOptionsPanel.GetComponent<Image>().fillAmount = x, 1, 0.4f);
            _isOpen = true;
        }
        else
        {
            DOTween.To(() => _powerUpOptionsPanel.GetComponent<Image>().fillAmount, x => _powerUpOptionsPanel.GetComponent<Image>().fillAmount = x, 0, 0.4f);
            _isOpen = false;
        }
    }

    void RefreshOwnedPowerups()
    {
        _ownedLarryText.text = SaveManager.Instance._saveData._playerData._ownedLarry.ToString();
        _ownedMrLeafText.text = SaveManager.Instance._saveData._playerData._ownedMrLeaf.ToString();
        _ownedWizardText.text = SaveManager.Instance._saveData._playerData._ownedWizard.ToString();
    }

    void OnPowerUpClick(GameManager.Powerup type)
    {
        if (!_canOpen) return;
        _canOpen = false;
        Invoke(nameof(EnableCanOpen), 1f);
        PowerUpPanel(false);
        switch(type)
        {
            case GameManager.Powerup.MrLeaf:
                MrLeafPowerUp();
                break;
            case GameManager.Powerup.LarryTheBird:
                LarryPowerup();
                break;
            case GameManager.Powerup.WizardTurtle:
                WizardPowerUp();
                break;
        }
    }

    void EnableCanOpen()
    {
        _canOpen = true;
    }

    void MrLeafPowerUp()
    {
        if (SaveManager.Instance._saveData._playerData._ownedMrLeaf <= 0) return;
        SaveManager.Instance._saveData.ReducePowerup(GameManager.Powerup.MrLeaf);
        RefreshOwnedPowerups();
        _MrLeafAnimObject.SetActive(true);
        _MrLeafAnimObject.GetComponent<Animator>().Play("anim");
        GameManager.Instance.AddLeafs(500);
        Invoke(nameof(DisableMrLeaf), 1.25f);
    }

    void DisableMrLeaf()
    {
        _MrLeafAnimObject.GetComponent<Animator>().Play("Idle");
        _MrLeafAnimObject.SetActive(false);
    }

    void WizardPowerUp()
    {
        if (SaveManager.Instance._saveData._playerData._ownedWizard <= 0) return;
        SaveManager.Instance._saveData.ReducePowerup(GameManager.Powerup.WizardTurtle);
        RefreshOwnedPowerups();
        _WizardAnimObject.SetActive(true);
        _WizardAnimObject.GetComponent<Animator>().Play("anim");
        Invoke(nameof(DisableWizard), 1.4f);

        GameObject[] enemyPillars = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject pillar in enemyPillars)
        {
            if (pillar.GetComponent<Caterpillar>()._caterPillarSpeed <= 3) return;
            pillar.GetComponent<Caterpillar>()._caterPillarSpeed -= 1f;
        }
    }

    void DisableWizard()
    {
        _WizardAnimObject.GetComponent<Animator>().Play("Idle");
        _WizardAnimObject.SetActive(false);
    }

    void LarryPowerup()
    {
        if (SaveManager.Instance._saveData._playerData._ownedLarry <= 0) return;
        SaveManager.Instance._saveData.ReducePowerup(GameManager.Powerup.LarryTheBird);
        RefreshOwnedPowerups();

        GameObject[] enemyPillars = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject pillar in enemyPillars)
        {

            GameObject poo = Instantiate(_poopObject, pillar.transform.position, Quaternion.identity);
            pillar.GetComponent<CaterpillarHealthManager>().DecreaseHealth(0.3f);
            Destroy(poo, 1f);
        }
    }
}
