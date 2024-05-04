using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    [Header("Caterpillar Modifiers")]
    public float _caterPillarSpeed = 5;
    public float _caterPillarHealth = 1;
    public float _caterPillarSpecialAttackRange = 15;
    public float _caterPillarBiteAttackRange = 5;
    public float _caterPillarSpecialAttackDamage = 10;
    public float _caterPillarBiteAttackDamage = 4;
    public float _caterPillarDamageModifier = 1;
    public float _caterPillarDamageTakenModifier = 1;

    [Tooltip("1 for right and -1 for left")]
    public float _dir;
    [Header("Boss Dependencies")]
    internal Animator _animator;
    [SerializeField] string _idleAnimName;
    [SerializeField] string _movementAnimName;

    /*[HideInInspector]*/
    public bool _canMove;
    bool _isPlayingMoveAnim = false;
    LayerMask _enemyLayer;

    public GameObject _enemy;
    public bool _isEnemyInRange;

    private bool _lockCaterpillarMovement;

    internal Action Attack1; // Special Attack
    internal Action Attack2; // Bite Attack

    [SerializeField]internal bool _isUsingAttack1;
    [SerializeField] internal bool _isUsingAttack2;


    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyLayer = (1 << 9); // bit shifts to create a layermask containing players layer
    }

    private void Move()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, new Vector2(transform.position.x + 0.5f * _dir, transform.position.y), Time.deltaTime * _caterPillarSpeed);
        transform.position = newPos;

        if(!_isPlayingMoveAnim)
        {
            _animator.CrossFadeInFixedTime(_movementAnimName, 0.5f);
            _isPlayingMoveAnim = true;
        }
    }

    protected virtual void Update()
    {
        if (_canMove)
            Move();

        if(!_canMove && _isPlayingMoveAnim)
        {
            _animator.CrossFadeInFixedTime(_idleAnimName, 1f);
            _isPlayingMoveAnim = false;
        }

        if (_enemy == null)
            CheckForEnemy();

        DetermineAction();

        if (GameManager.Instance._debugRays)
        {
            Vector3 range1 = transform.TransformDirection(Vector3.right * _dir) * _caterPillarBiteAttackRange;
            Debug.DrawRay(transform.position, range1, Color.green);

            Vector3 range2 = transform.TransformDirection(Vector3.right * _dir) * _caterPillarSpecialAttackRange;
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), range2, Color.green);
        }
    }

    void CheckForEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * _dir, Mathf.Infinity, ~_enemyLayer); //~_playerLayer means all layers except _playerLayer
        if (hit)
        {
            Debug.Log(gameObject.name + " has hit " + hit.collider.name);
            if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("PlayerBase"))
                _enemy = hit.collider.gameObject;
        }
    }

    void DetermineAction()
    {
        if(_enemy == null)
        {
            _canMove = true;
            return;
        }

        float distance = Vector2.Distance(_enemy.transform.position, transform.position);

        if (distance > _caterPillarSpecialAttackRange)
        {
            _canMove = true;
            return;
        }

        if(distance < _caterPillarSpecialAttackRange && distance > _caterPillarBiteAttackRange && !_isUsingAttack1 && !_isUsingAttack2)
        {
            _canMove = false;
            Attack1.Invoke();
            _isUsingAttack1 = true;
            return;
        }

        if (distance < _caterPillarBiteAttackRange && !_isUsingAttack1 && !_isUsingAttack2)
        {
            _canMove = false;
            Attack2.Invoke();
            _isUsingAttack2 = true;
            return;
        }
    }

    public void ReleaseCaterPillar()
    {
        if (_lockCaterpillarMovement) return; //return if movement locked
        _canMove = true;
    }

    public void LockCaterpillar(bool state)
    {
        _lockCaterpillarMovement = state;
    }
    public void SetEnemyNull()
    {
        _enemy = null;
    }
}
