using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Honey : MonoBehaviour
{
    Caterpillar cp;
    [SerializeField] float _honeyEffectTime;
    [Tooltip("0 for no speed reduce, 5 for 0 speed")]
    [SerializeField] float _honeyReduceSpeedAmount;
    [SerializeField] float _range;
    [Tooltip("Delay between every attack")]
    [SerializeField] float _fireRate;
    [SerializeField] GameObject _honey;
    [SerializeField] float _honeySpeed;
    [SerializeField] GameObject _honeyObject;
    bool _canAttack = true;
    private void Start()
    {
        cp = GetComponentInParent<Caterpillar>();
    }
    private void Update()
    {
        if (GameManager.Instance._debugRays)
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
        GameObject cb = Instantiate(_honey, transform.position, Quaternion.identity);
        cb.transform.DOJump(cp._enemy.transform.position, 2, 1, Vector3.Distance(cb.transform.position, cp._enemy.transform.position) / _honeySpeed);
        Destroy(cb, (Vector3.Distance(cb.transform.position, cp._enemy.transform.position) / _honeySpeed) - 0.2f);
        Invoke("EnableAttack", _fireRate);
        Invoke(nameof(CreateHoneyOnEnemy), (Vector3.Distance(cb.transform.position, cp._enemy.transform.position) / _honeySpeed) - 0.2f);
    }

    void CreateHoneyOnEnemy()
    {
        cp._caterPillarSpeed -= _honeyReduceSpeedAmount;
        GameObject h = Instantiate(_honeyObject, cp._enemy.transform.position, Quaternion.identity, cp._enemy.transform);
        h.GetComponent<HoneyObject>().StartFading(_honeyEffectTime, _honeyReduceSpeedAmount);
    }

    void EnableAttack()
    {
        cp.ReleaseCaterPillar();
        _canAttack = true;
    }
}
