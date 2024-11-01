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
        if (cp._enemy == null)
        {
            return;
        }

        if (Vector2.Distance(transform.position, cp._enemy.transform.position) < _range)
            Attack();
        
    }
    GameObject cb;
    void Attack()
    {
        if (!_canAttack) return;
        if (cp._enemy == null)
            return;
        cp._canMove = false;
        cp.InvokeStopAnim();
        _canAttack = false;
        DamageEnemy();
        AttackAnim();
        _as.Play();
        cb = Instantiate(_cannonBall, transform.position, Quaternion.identity);
        float travelTime = Vector3.Distance(cb.transform.position, cp._enemy.transform.position) / _cannonBallSpeed;
        cb.transform.DOJump(cp._enemy.transform.position, 2, 1, travelTime);
        cb.GetComponent<ProjectileDestroyer>().DestroySelfInTime(travelTime);
        Invoke(nameof(EnableMovement), travelTime);
        Invoke("EnableAttack", _fireRate);
    }
    void DamageEnemy()
    {
        if(cp._enemy.CompareTag("Enemy") || cp._enemy.CompareTag("Player"))
        {
            float damage = (_damage / 10);
            cp._enemy?.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);
        }
        if (cp.enemy == null)
            return;

        if (cp._enemy.CompareTag("EnemyBase") || cp._enemy.CompareTag("PlayerBase"))
        {
            float damage = _damage / 10;
            cp._enemy?.GetComponent<CaterpillarBase>()?.ReduceHealth(damage);
        }
    }

    void AttackAnim()
    {
        transform.DOLocalMoveX(-0.26f, 0.3f).SetEase(Ease.InOutBack).SetLoops(2, LoopType.Yoyo).OnComplete(()=> {
            cp.ReleaseCaterPillar();
        });   
    }

    void EnableMovement()
    {
       /* cp._canMove = true;
        cp.ReleaseCaterPillar();*/
        /*Destroy(cb);*/
    }

    void EnableAttack()
    {
        _canAttack = true;
    }
}
