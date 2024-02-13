using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grenadier : MonoBehaviour
{
    Caterpillar cp;
    [SerializeField] float _damage;
    [SerializeField] float _range;
    [Tooltip("Delay between every attack")]
    [SerializeField] float _fireRate;
    [SerializeField]GameObject _bullet;
    [SerializeField] float _bulletSpeed;
    bool _canAttack = true;
    [SerializeField] AudioSource _as;
    private void Start()
    {
        cp = GetComponentInParent<Caterpillar>();
    }
    private void Update()
    {
        if(GameManager.Instance._debugRays)
        {
            Vector3 range = transform.TransformDirection(Vector3.right) * _range;
            Debug.DrawRay(transform.position, range, Color.green);
        }
        if (cp._enemy == null) return;

        if (Vector2.Distance(transform.position, cp._enemy.transform.position) < _range)
            Attack();
        
    }

    void Attack()
    {
        if (!_canAttack) return;
        cp._canMove = false;
        cp.InvokeStopAnim();
        _canAttack = false;
        DamageEnemy();
        AttackAnim();
        _as.Play();
        GameObject cb = Instantiate(_bullet, transform.position, Quaternion.identity);
        cb.transform.DOMove(cp._enemy.transform.position, Vector3.Distance(cb.transform.position, cp._enemy.transform.position) / _bulletSpeed).OnComplete(() => Destroy(cb));
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
    void AttackAnim()
    {
        transform.DOLocalMoveX(-0.26f, 0.18f).SetEase(Ease.InOutBack).SetLoops(2, LoopType.Yoyo).OnComplete(()=> {
            cp.ReleaseCaterPillar();
        });   
    }

    void EnableAttack()
    {
        _canAttack = true;
    }
}
