using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Geraldine : Boss
{
    [Header("Sgt.Flowerpuff Dependencies")]
    [SerializeField] GameObject _cannons;
    [SerializeField] GameObject _projectile;
    protected override void Start()
    {
        base.Start();

        Attack1 += AttackSpecial;
        Attack2 += AttackBite;

        _canMove = true;
    }

    protected override void Update()
    {
        base.Update();
    }

    void AttackSpecial()
    {
        StartCoroutine(FireCannon());
    }
    IEnumerator FireCannon()
    {
        _cannons.transform.DOLocalMoveX(-0.26f, 0.3f).SetEase(Ease.InOutBack).SetLoops(2, LoopType.Yoyo);

        GameObject projectile1 = Instantiate(_projectile, _cannons.transform.position, Quaternion.identity);

        projectile1.transform.DOJump(_enemy.transform.position, 2, 1, Vector3.Distance(_cannons.transform.position, _enemy.transform.position) / 12).OnComplete(() => Destroy(projectile1));

        float timeToDeath = Vector3.Distance(_cannons.transform.position, _enemy.transform.position) / 12;

        yield return new WaitForSeconds(timeToDeath);

        if (_enemy.CompareTag("Player"))
        {
            float damage = _caterPillarSpecialAttackDamage / 10;
            _enemy?.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);
        }
        if (_enemy.CompareTag("PlayerBase"))
        {
            float damage = _caterPillarSpecialAttackDamage / 10;
            _enemy?.GetComponent<CaterpillarBase>()?.ReduceHealth(damage);
        }

        _animator.CrossFadeInFixedTime("Idle", 0.5f);
        _isUsingAttack1 = false;
    }

    void AttackBite()
    {
        _animator.CrossFadeInFixedTime("AttackBite", 0.5f);
        if (_enemy.CompareTag("Player"))
        {
            float damage = _caterPillarSpecialAttackDamage / 10;
            _enemy?.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);
        }
        if (_enemy.CompareTag("PlayerBase"))
        {
            float damage = _caterPillarSpecialAttackDamage / 10;
            _enemy?.GetComponent<CaterpillarBase>()?.ReduceHealth(damage);
        }
        Invoke(nameof(EnableAttack2), 1f);
    }

    void EnableAttack2()
    {
        _animator.CrossFadeInFixedTime("Idle", 0.5f);
        _isUsingAttack2 = false;
    }
}
