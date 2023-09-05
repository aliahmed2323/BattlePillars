using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CaterpillarHealthManager : MonoBehaviour
{
     private float _health;
    [SerializeField] float _damagePunchEffectModifier;

    [HideInInspector]
    public delegate void onDeath();
    [HideInInspector]
    public event onDeath _onDeath;

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
        _health -= amount;
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
        _onDeath.Invoke();
        Destroy(gameObject);
    }
}
