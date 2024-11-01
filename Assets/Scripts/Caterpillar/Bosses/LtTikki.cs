using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LtTikki : Boss
{
    [Header("Lt.Tikki-Takka Dependencies")]
    [SerializeField] GameObject[] _spikes;

    [SerializeField] AudioSource _as;
    [SerializeField] AudioClip[] _clips;
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
        StartCoroutine(LaunchSpikes());
    }

    List<Vector2> spikePos = new();
    IEnumerator LaunchSpikes()
    {
        _as.clip = _clips[1];
        _as.Play();
        foreach (GameObject spike in _spikes)
        {
            spikePos.Add(spike.transform.position);
            spike.transform.DOJump(_enemy.transform.position, 4, 1, 1.5f);
        }

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject spike in _spikes)
        {
            spike.transform.DOLookAt(_enemy.transform.position, 0.7f);
        }

        yield return new WaitForSeconds(0.9f);

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

        int k = 0;
        foreach (GameObject spike in _spikes)
        {
            spike.transform.position = spikePos[k];
            spike.transform.Rotate(new Vector3(0, 0, 0), Space.Self);
            k++;
        }
    }

    void AttackBite()
    {
        _as.clip = _clips[0];
        _as.Play();
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
        _isUsingAttack2 = false;
        _animator.CrossFadeInFixedTime("Idle", 0.5f);
    }
}
