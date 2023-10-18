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
    [SerializeField]GameObject _cannonBall;
    [SerializeField] float _cannonBallSpeed;
    [SerializeField] AudioSource _as;
    bool _canAttack = true;
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
        float damage = ((_damage / 10) * GetComponentInParent<Caterpillar>()._caterPillarDamageModifier) * cp._enemy.GetComponent<Caterpillar>()._caterPillarDamageTakenModifier;
        cp._enemy?.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);
        AttackAnim();
        _as.Play();
        GameObject cb = Instantiate(_cannonBall, transform.position, Quaternion.identity);
        cb.transform.DOJump(cp._enemy.transform.position, 2, 1, Vector3.Distance(cb.transform.position, cp._enemy.transform.position) / _cannonBallSpeed).OnComplete(() => Destroy(cb));
        Invoke("EnableAttack", _fireRate);
    }

    void AttackAnim()
    {
        transform.DOLocalMoveX(-0.26f, 0.3f).SetEase(Ease.InOutBack).SetLoops(2, LoopType.Yoyo).OnComplete(()=> {
            cp.ReleaseCaterPillar();
        });   
    }

    void EnableAttack()
    {
        _canAttack = true;
    }
}
