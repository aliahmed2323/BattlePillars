using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CampaignScreen : MonoBehaviour
{
    [SerializeField] Transform[] _trees;
    List<Tween> _treeAnim = new();

    private void Start()
    {
        AnimateTree();
    }

    private void OnEnable()
    {
        DOTween.PlayAll();
    }

    private void OnDisable()
    {
        DOTween.PauseAll();
    }

    void AnimateTree()
    {
        foreach(Transform child in _trees)
        {
            _treeAnim.Add(child.DOScale(1.05f, 1.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InSine));
        }
    }
}
