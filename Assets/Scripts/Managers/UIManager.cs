using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public CaterpillarBuilderPanel _caterPillarBuilderPanel;
    public MainMenuPanel _mainMenuPanel;
    public GameModeSelectionPanel _gamemodeSelectionPanel;
    public CampaignScreen _campaignScreen;
    public SegmentSelection _segmentSelectionPanel;
    public SegmentUpgradeScreen _segmentUpgradePanel;

    public CaterpillarsScriptableObject _caterpillar;

    public enum Screens
    {
        MainMenu = 0,
        GameModeSelection = 1,
        CampaignScreen = 2,
        SegmentSelection = 3,
        SegmentUpgrade = 4
    }


    private void Start()
    {
        CheckPlayerPrefsToEnablePanel();
    }

    void CheckPlayerPrefsToEnablePanel()
    {
        if (!PlayerPrefs.HasKey("PanelEnabler"))
            return;

        _mainMenuPanel.gameObject.SetActive(false);

        int panelToEnable = PlayerPrefs.GetInt("PanelEnabler");
        PlayerPrefs.DeleteKey("PanelEnabler");

        switch(panelToEnable)
        {
            case 0:
                _mainMenuPanel.gameObject.SetActive(true);
                break;
            case 1:
                _gamemodeSelectionPanel.gameObject.SetActive(true);
                break;
            case 2:
                _campaignScreen.gameObject.SetActive(true);
                break;
            case 3:
                _segmentSelectionPanel.gameObject.SetActive(true);
                break;
            case 4:
                _segmentUpgradePanel.gameObject.SetActive(true);
                break;
        }
    }
}
