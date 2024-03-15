using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentUpgradeScreen : MonoBehaviour
{
    public GameObject _textbox;
    [SerializeField] Button _purchaseButton;
    [HideInInspector]
    public GameObject _selectedSegmentGameObject;
    [HideInInspector]
    public GameManager.SegmentType _selectedSegment;
    [HideInInspector]
    public int _selectedSegmentCost;

    private void Start()
    {
        _purchaseButton.onClick.AddListener(() => PurchaseSegment());
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
    }
}
