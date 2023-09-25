using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CaterpillarHealthManager : MonoBehaviour
{
     private float _health;
    [SerializeField] float _damagePunchEffectModifier;
    [SerializeField] Slider _healthSlider;


    private void Start()
    {
        _health = GetComponent<Caterpillar>()._caterPillarHealth;
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

    void HurtAnim()
    {
        //transform.DOPunchScale(new Vector3(transform.localScale.x + _damagePunchEffectModifier, transform.localScale.y + _damagePunchEffectModifier, transform.localScale.z),0.25f);
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
