using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Frankenpillar : Boss
{
    [Header("Frankenpillar Dependencies")]
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
        StartCoroutine(LightningAttack());
    }

    IEnumerator LightningAttack()
    {
        _animator.CrossFadeInFixedTime("Lightning", 0.2f);
        yield return new WaitForSeconds(0.5f);

        GameObject l = Instantiate(_projectile, new Vector2(_enemy.transform.position.x, _enemy.transform.position.y + 4), Quaternion.identity);
        Destroy(l, 0.2f);
        yield return new WaitForSeconds(0.2f);
        float damage = _caterPillarSpecialAttackDamage / 10;
        _enemy?.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);

        _isUsingAttack1 = false;
    }

    void AttackBite()
    {
        _animator.CrossFadeInFixedTime("AttackBite", 0.5f);
        float damage = _caterPillarBiteAttackDamage / 10;
        _enemy?.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);
        Invoke(nameof(EnableAttack2), 1f);
    }

    void EnableAttack2()
    {
        _isUsingAttack2 = false;
    }
}
