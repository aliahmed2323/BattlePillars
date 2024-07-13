using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class BaseWeapon : MonoBehaviour
{
    public Caterpillar cp;
    [SerializeField] float _damage;
    [SerializeField] float _range;
    [Tooltip("Delay between every attack")]
    [SerializeField] float _fireRate;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _bulletSpeed;
    bool _canAttack = false;
    [SerializeField] AudioSource _as;
    [SerializeField] Transform _projectileTransform;
    [SerializeField] GameObject _gun;

    private void Update()
    {
        if (GameManager.Instance._debugRays)
        {
            Vector3 range = transform.TransformDirection(Vector3.right) * _range;
            Debug.DrawRay(transform.position, range, Color.green);
        }
        if (cp == null) return;

        if (Vector2.Distance(transform.position, cp._enemy.transform.position) < _range)
            Attack();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("trigger enter");
            _canAttack = true;
            cp = collision.gameObject.GetComponent<Caterpillar>();
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("trigger exit");
            _canAttack = false;
            cp = null;
        }
    }*/

    void Attack()
    {
        if (!_canAttack) return;
        _canAttack = false;
        DamageEnemy();
        AttackAnim();
        _as.Play();
        GameObject cb = Instantiate(_bullet, _projectileTransform.position, Quaternion.identity);
        cb.transform.DOMove(cp._enemy.transform.position, Vector3.Distance(cb.transform.position, cp._enemy.transform.position) / _bulletSpeed).OnComplete(() => Destroy(cb));
        Invoke("EnableAttack", _fireRate);
    }
    void DamageEnemy()
    {
            float damage = _damage / 10;
            cp.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);
    }
    void AttackAnim()
    {
        _gun.transform.DOLocalMoveX(-0.06f, 0.18f).SetEase(Ease.InOutBack).SetLoops(2, LoopType.Yoyo);
    }

    void EnableAttack()
    {
        _canAttack = true;
    }
}
