using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grenadier : MonoBehaviour
{
    Caterpillar cp;
    [SerializeField] float _damage;
    [SerializeField] float _rangeMaximum;
    [SerializeField] float _rangeMinimum;
    [Tooltip("Delay between every attack")]
    [SerializeField] float _fireRate;
    [SerializeField] GameObject _grenade;
    [SerializeField] float _grenadeSpeed;
    [SerializeField] GameObject _explosionEffect;
    bool _canAttack = true;
    [SerializeField] AudioSource _as;
    private void Start()
    {
        cp = GetComponentInParent<Caterpillar>();
    }
    private void Update()
    {
        if (GameManager.Instance._debugRays)
        {
            Vector3 range = transform.TransformDirection(Vector3.right) * _rangeMaximum;
            Debug.DrawRay(transform.position, range, Color.green);
        }
        if (cp._enemy == null) return;

        if (Vector2.Distance(transform.position, cp._enemy.transform.position) < _rangeMaximum && Vector2.Distance(transform.position, cp._enemy.transform.position) > _rangeMinimum)
            Attack();


    }

    void Attack()
    {
        if (!_canAttack) return;
        cp._canMove = false;
        cp.InvokeStopAnim();
        _canAttack = false;
        DamageEnemy();
        _as.Play();
        GameObject cb = Instantiate(_grenade, _grenade.transform.position, _grenade.transform.rotation);
        _grenade.SetActive(false);
        float timeToHit = Vector3.Distance(cb.transform.position, cp._enemy.transform.position) / _grenadeSpeed;
        cb.transform.DOJump(cp._enemy.transform.position, 4, 1, timeToHit).OnComplete(() =>
        {
            ExplosionEffect(cb.transform.position);
            Destroy(cb);
        });

        Invoke("EnableAttack", _fireRate);
    }
    void DamageEnemy()
    {
        if (cp._enemy.CompareTag("Enemy") || cp._enemy.CompareTag("Player"))
        {
            float damage = ((_damage / 10) * GetComponentInParent<Caterpillar>()._caterPillarDamageModifier) * cp._enemy.GetComponent<Caterpillar>()._caterPillarDamageTakenModifier;
            cp._enemy?.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);
        }
        if (cp._enemy.CompareTag("EnemyBase") || cp._enemy.CompareTag("PlayerBase"))
        {
            float damage = ((_damage / 10) * GetComponentInParent<Caterpillar>()._caterPillarDamageModifier);
            cp._enemy?.GetComponent<CaterpillarBase>()?.ReduceHealth(damage);
        }
    }
    void ExplosionEffect(Vector3 pos)
    {
        GameObject e = Instantiate(_explosionEffect, pos, Quaternion.identity);
        e.GetComponent<Animator>().Play("Explosion");
        Destroy(e, 1.1f);
    }
    void EnableAttack()
    {
        _canAttack = true;
        _grenade.SetActive(true);
    }
}
