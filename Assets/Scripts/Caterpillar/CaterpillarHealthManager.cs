using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CaterpillarHealthManager : MonoBehaviour
{
     public float _health;
    [SerializeField] float _damagePunchEffectModifier;
    public Slider _healthSlider;

    [SerializeField] bool _isBoss = false;
    public delegate void OnDeath();
    public event OnDeath onDeath;

    private void Start()
    {
        Invoke(nameof(SetVals), 0.1f);
    }
    public void SetVals()
    {
        if (!_isBoss)
            _health = GetComponent<Caterpillar>()._caterPillarHealth;
        else
            _health = GetComponent<Boss>()._caterPillarHealth;
        _healthSlider.maxValue = _health;
    }

    public float GetHealth()
    {
        return _health;
    }

    public void DecreaseHealth(float amount)
    {
        _healthSlider.gameObject.SetActive(true);
        _health -= amount;
        _healthSlider.value = Mathf.Lerp(_health + amount, _health, Time.deltaTime * 3);
        HurtAnim();

        if (_health <= 0)
            Die();
    }

    public void AddHealth(float amount)
    {
        
        _health += amount;
        _healthSlider.value = Mathf.Lerp(_health - amount, _health, Time.deltaTime * 3);

        if(GetComponent<Caterpillar>()._caterPillarHealth >= _health)
            _healthSlider.gameObject.SetActive(false);
    }

    void HurtAnim()
    {
        //transform.DOPunchScale(new Vector3(transform.localScale.x + _damagePunchEffectModifier, transform.localScale.y + _damagePunchEffectModifier, transform.localScale.z),0.25f);
    }

    void Die()
    {
        if (gameObject.CompareTag("Enemy"))
            GameManager.Instance.kills++;

        /*onDeath.Invoke();*/
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
