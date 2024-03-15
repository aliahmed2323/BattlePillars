using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CampaignScreen : MonoBehaviour
{
    [SerializeField] Transform[] _trees;
    [SerializeField] GameObject _popup;
    public GameObject _selectedLevel;
    List<Tween> _treeAnim = new();
    [SerializeField] GameObject[] _levels;

    private void Start()
    {
        AnimateTree();
        LoadLevelsFromSave();
    }

    private void OnEnable()
    {
        DOTween.PlayAll();
    }

    private void OnDisable()
    {
        DOTween.PauseAll();
    }

    public void EnablePopup(string mapname, int level, int reward)
    {
        _popup.SetActive(true);
        _popup.GetComponent<CampiagnLevelPopup>().UpdatePopup(mapname, level, reward);
    }

    void AnimateTree()
    {
        foreach(Transform child in _trees)
        {
            _treeAnim.Add(child.DOScale(1.05f, 1.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InSine));
        }
    }

    public void LoadLevelsFromSave()
    {
        int count = SaveManager.Instance._saveData._playerData._completedLevels.Count;

        for(int i = 0; i <= count; i++)
        {
            _levels[i].GetComponent<CampaignMenuLevels>()._isLocked = false;
        }
    }
}
