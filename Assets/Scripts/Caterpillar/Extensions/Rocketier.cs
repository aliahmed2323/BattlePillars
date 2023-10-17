using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocketier : MonoBehaviour
{
    Caterpillar cp;
    [SerializeField] float _damage;
    [SerializeField] float _rangeMaximum;
    [SerializeField] float _rangeMinimum;
    [Tooltip("Delay between every attack")]
    [SerializeField] float _fireRate;
    [SerializeField]GameObject _rocket;
    [SerializeField] float _rocketSpeed;
    bool _canAttack = true;
    private void Start()
    {
        cp = GetComponentInParent<Caterpillar>();
    }
    private void Update()
    {
        if(GameManager.Instance._debugRays)
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
        float damage = ((_damage / 10) * GetComponentInParent<Caterpillar>()._caterPillarDamageModifier) * cp._enemy.GetComponent<Caterpillar>()._caterPillarDamageTakenModifier;
        cp._enemy?.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);

        GameObject cb = Instantiate(_rocket, transform.position, Quaternion.identity);
        float timeToHit = Vector3.Distance(cb.transform.position, cp._enemy.transform.position) / _rocketSpeed;
        cb.transform.DOJump(cp._enemy.transform.position, 4, 1, timeToHit).OnComplete(() => Destroy(cb));
        /*cb.transform.DOLookAt(cp._enemy.transform.position, timeToHit, AxisConstraint.Z);*/
        Invoke("EnableAttack", _fireRate);
    }


    void EnableAttack()
    {
        _canAttack = true;
    }
}
