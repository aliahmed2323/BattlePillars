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
