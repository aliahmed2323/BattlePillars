using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Caterpillar Modifiers")]
    public float _caterPillarSpeed = 5;
    public float _caterPillarHealth = 1;
    public float _caterPillarDamageModifier = 1;
    public float _caterPillarDamageTakenModifier = 1;

    [Tooltip("1 for right and -1 for left")]
    public float _dir;
    [Header("Boss Dependencies")]
    [SerializeField] Animator _animator;
    [SerializeField] string _idleAnimName;
    [SerializeField] string _movementAnimName;

    /*[HideInInspector]*/
    public bool _canMove;
    bool _isPlayingMoveAnim = false;

    public GameObject _enemy;
    public bool _isEnemyInRange;

    private bool _lockCaterpillarMovement;


    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
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
