using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Canon : MonoBehaviour
{
    Caterpillar cp;
    [SerializeField] float _damage;
    [SerializeField] float _range;
    [Tooltip("Delay between every attack")]
    [SerializeField] float _fireRate;
    bool _canAttack = true;
    private void Start()
    {
        cp = GetComponentInParent<Caterpillar>();
    }
    private void Update()
    {
        if (cp._enemy == null) return;

        if (Vector2.Distance(transform.position, cp._enemy.transform.position) < _range)
            Attack();
        
    }

    void Attack()
    {
        if (!_canAttack) return;
        _canAttack = false;
        float damage = ((_damage / 10) * GetComponentInParent<Caterpillar>()._caterPillarDamageModifier) * cp._enemy.GetComponent<Caterpillar>()._caterPillarDamageTakenModifier;
        cp._enemy.GetComponent<CaterpillarHealthManager>().DecreaseHealth(damage);
        transform.DOPunchScale(new Vector3(transform.localScale.x + 0.02f, transform.localScale.y + 0.02f, transform.localScale.z), 0.3f);
        Invoke("EnableAttack", _fireRate);
    }

    void EnableAttack()
    {
        _canAttack = true;
    }
}
