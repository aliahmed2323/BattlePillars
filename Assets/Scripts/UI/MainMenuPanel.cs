using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] Button _playButton;

    [SerializeField] Image _playerPillarHead;
    [SerializeField] Image _playerPillarGunBody;
    [SerializeField] Image _playerPillarBody;

    [SerializeField] Image _enemyPillarHead;
    [SerializeField] Image _enemyPillarGunBody;
    [SerializeField] Image _enemyPillarBody;

    List<Tween> _battlePillarAnims = new();
    private void Start()
    {
        _playButton.onClick.AddListener(() => Invoke("OpenMainMenuPanel", 0.25f));
        UIManager.Instance._mainMenuPanel = this;
        AnimateBattlePillars();
    }
    private void OnEnable()
    {
        foreach(Tween t in _battlePillarAnims)
        {
            t.Play();
        }
    }
    private void OnDisable()
    {
        foreach (Tween t in _battlePillarAnims)
        {
            t.Pause();
        }
    }
    void AnimateBattlePillars()
    {
        // Playerpillar Scale Anim
        _battlePillarAnims.Add(_playerPillarHead.transform.DOScale(_playerPillarHead.transform.localScale + new Vector3(0.1f, 0.1f,0f), 1.8f).SetLoops(-1, LoopType.Yoyo));
        _battlePillarAnims.Add(_playerPillarGunBody.transform.DOScale(_playerPillarGunBody.transform.localScale + new Vector3(0.1f, 0.1f,0f), 1.8f).SetDelay(0.25f).SetLoops(-1, LoopType.Yoyo));
        _battlePillarAnims.Add(_playerPillarBody.transform.DOScale(_playerPillarBody.transform.localScale + new Vector3(0.1f, 0.1f,0f), 1.8f).SetDelay(0.45f).SetLoops(-1, LoopType.Yoyo));
        // Playerpillar Position Anim
        _battlePillarAnims.Add(_playerPillarHead.transform.DOMove(_playerPillarHead.transform.position + new Vector3(0f, 40f, 0f), 1.8f).SetLoops(-1, LoopType.Yoyo));
        _battlePillarAnims.Add(_playerPillarGunBody.transform.DOMove(_playerPillarGunBody.transform.position + new Vector3(0f, 40f, 0f), 1.8f).SetDelay(0.25f).SetLoops(-1, LoopType.Yoyo));
        _battlePillarAnims.Add(_playerPillarBody.transform.DOMove(_playerPillarBody.transform.position + new Vector3(0f, 40f, 0f), 1.8f).SetDelay(0.45f).SetLoops(-1, LoopType.Yoyo));
        // EnemyPillar Scale Anim
        _battlePillarAnims.Add(_enemyPillarHead.transform.DOScale(_enemyPillarHead.transform.localScale + new Vector3(0.1f, 0.1f, 0f), 1.8f).SetLoops(-1, LoopType.Yoyo));
        _battlePillarAnims.Add(_enemyPillarGunBody.transform.DOScale(_enemyPillarGunBody.transform.localScale + new Vector3(0.1f, 0.1f, 0f), 1.8f).SetDelay(0.25f).SetLoops(-1, LoopType.Yoyo));
        _battlePillarAnims.Add(_enemyPillarBody.transform.DOScale(_enemyPillarBody.transform.localScale + new Vector3(0.1f, 0.1f, 0f), 1.8f).SetDelay(0.45f).SetLoops(-1, LoopType.Yoyo));
        // EnemyPillar Position Anim
        _battlePillarAnims.Add(_enemyPillarHead.transform.DOMove(_enemyPillarHead.transform.position + new Vector3(0f, 40f, 0f), 1.8f).SetLoops(-1, LoopType.Yoyo));
        _battlePillarAnims.Add(_enemyPillarGunBody.transform.DOMove(_enemyPillarGunBody.transform.position + new Vector3(0f, 40f, 0f), 1.8f).SetDelay(0.25f).SetLoops(-1, LoopType.Yoyo));
        _battlePillarAnims.Add(_enemyPillarBody.transform.DOMove(_enemyPillarBody.transform.position + new Vector3(0f, 40f, 0f), 1.8f).SetDelay(0.45f).SetLoops(-1, LoopType.Yoyo));

    }

    void OpenMainMenuPanel()
    {
        UIManager.Instance._gamemodeSelectionPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
