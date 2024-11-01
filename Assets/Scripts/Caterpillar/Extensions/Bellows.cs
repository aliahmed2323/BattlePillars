using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bellows : MonoBehaviour
{

    Caterpillar cp;

    [SerializeField] GameObject _airObject;
    [SerializeField] Transform _shootPoint;
    [Tooltip("Attacks every x Seconds")]
    [SerializeField] float _rateOfFire;
    [Tooltip("Distance travelled per second")]
    [SerializeField] float _attackSpeed;
    [SerializeField] float _pushDistance;
    [SerializeField] float _rangeMaximum;
    [SerializeField] float _rangeMinimum;
    [SerializeField] bool _playAnim = true;

    [SerializeField] AudioSource _as;

    bool _canAttack = true;

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
            if (_canAttack)
                StartCoroutine(Attack());   


    }

    IEnumerator Attack()
    {
        _canAttack = false;
        if(_playAnim)
            GetComponent<Animator>().Play("Shoot");
        yield return new WaitForSeconds(0.8f);
        _as.Play();
        GameObject air = Instantiate(_airObject, _shootPoint.position, Quaternion.identity);
        air.GetComponent<ProjectileDestroyer>()._destroyType = ProjectileDestroyer.DestroyType.Enemy;
        air.GetComponent<SpriteRenderer>().DOFade(0, 0);

        float timeToReachTarget = Vector2.Distance(air.transform.position, cp._enemy.transform.position) / _attackSpeed;

        air.transform.DOMoveX(cp._enemy.transform.position.x, timeToReachTarget);
        air.GetComponent<SpriteRenderer>().DOFade(255, timeToReachTarget);

        yield return new WaitUntil(()=> air == null);

        cp._enemy.GetComponent<EnemyCaterpillar>()._canMove = false;
        cp._enemy.transform.DOJump(new Vector3(cp._enemy.transform.position.x + _pushDistance, cp._enemy.transform.position.y, cp._enemy.transform.position.z), 2, 1, 1.2f).OnComplete
            (
            ()=> cp._enemy.GetComponent<EnemyCaterpillar>()._canMove = true
            );
        Invoke(nameof(EnableAttack), _rateOfFire);
    }

    void EnableAttack()
    {
        _canAttack = true;
    }
}
