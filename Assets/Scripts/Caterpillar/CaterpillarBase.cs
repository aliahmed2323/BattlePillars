using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaterpillarBase : MonoBehaviour
{
    [SerializeField] float _health;
    [SerializeField] bool _isEnemyBase;
    [SerializeField] Slider _healthSlider;
    [SerializeField] GameOverPanel _gmp;

    [SerializeField] GameObject _attackUpgrade;
    [SerializeField] GameObject _defenceUpgrade;

    private void Start()
    {
        if(!_isEnemyBase)
            AddBaseUpgrades();
    }

    void AddBaseUpgrades()
    {
        if (SaveManager.Instance._saveData.IsBaseUpgradeOwned(GameManager.BaseUpgrades.Attack))
            _attackUpgrade.SetActive(true);

        if (SaveManager.Instance._saveData.IsBaseUpgradeOwned(GameManager.BaseUpgrades.Production))
        {
            LeafManager.Instance._leafGenMultiplier = 2;
        }

        if (SaveManager.Instance._saveData.IsBaseUpgradeOwned(GameManager.BaseUpgrades.Defense))
        {
            _health = _health * 2;
            _defenceUpgrade.SetActive(true);
        }
    }

    public void ReduceHealth(float amount)
    {
        _healthSlider.gameObject.SetActive(true);
        _health -= amount;
        _healthSlider.value = _health;
        if (_health <= 0)
            Die();
    }

    void Die()
    {
        _gmp.gameObject.SetActive(true);
        Time.timeScale = 0;
        if (_isEnemyBase)
            _gmp.EndGame(true);
        else
            _gmp.EndGame(false);
    }
}
