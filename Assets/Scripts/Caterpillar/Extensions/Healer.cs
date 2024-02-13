using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] float _healthBuff;
    [SerializeField] bool _healOverTime;
    [Tooltip("Health Gained every three seconds until max health")]
    [SerializeField] float _healOverTimeValue;

    private void Start()
    {
        GetComponentInParent<Caterpillar>()._caterPillarHealth += _healthBuff;
        if (_healOverTime)
            StartCoroutine(HealOverTime());
    }

    IEnumerator HealOverTime()
    {
        if(GetComponentInParent<Caterpillar>()._caterPillarHealth < GetComponentInParent<Caterpillar>()._caterPillarHealth + _healOverTimeValue)
        {
            float remainingHealth = (GetComponentInParent<Caterpillar>()._caterPillarHealth - GetComponentInParent<CaterpillarHealthManager>().GetHealth());
            GetComponentInParent<CaterpillarHealthManager>().AddHealth(remainingHealth);
        }
        else
            GetComponentInParent<CaterpillarHealthManager>().AddHealth(_healOverTimeValue);

        yield return new WaitForSeconds(3);

        StartCoroutine(HealOverTime());
    }
}
