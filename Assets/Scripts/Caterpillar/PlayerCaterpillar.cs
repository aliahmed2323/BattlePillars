using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaterpillar : Caterpillar
{

    LayerMask _playerLayer;

    private void Start()
    {
        _playerLayer = (1 << 8); // bit shifts to create a layermask containing player's layer
        _extensions.Add(transform.GetChild(0).gameObject);
    }

    protected override void Update()
    {
        base.Update();
        if (ReferenceEquals(_enemy, null))
            CheckForEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (enemy != null)
                _enemy.GetComponent<CaterpillarHealthManager>().onDeath -= ResetBattlepillarToAttackState;

            _enemy = collision.gameObject;
            _canMove = false;
            InvokeStopAnim();
            LockCaterpillar(true);
            base._isEnemyInRange = true;
            _enemy.GetComponent<CaterpillarHealthManager>().onDeath += ResetBattlepillarToAttackState;
        }

        if(collision.gameObject.CompareTag("EnemyBase"))
        {
            _enemy = collision.gameObject;
            _canMove = false;
            InvokeStopAnim();
            LockCaterpillar(true);
            _hasReachBase = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBase"))
        {
            LockCaterpillar(false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LockCaterpillar(false);
        }
    }

    void CheckForEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * _dir, Mathf.Infinity, ~_playerLayer); //~_playerLayer means all layers except _playerLayer
        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                if (_enemy != null)
                    ResetBattlepillarToAttackState();
                if (enemy != null)
                    _enemy.GetComponent<CaterpillarHealthManager>().onDeath -= ResetBattlepillarToAttackState;

                _isEnemyInRange = true;
                _enemy = hit.collider.gameObject;  //problem point
                _enemy.GetComponent<CaterpillarHealthManager>().onDeath += ResetBattlepillarToAttackState;

            }

            if (hit.collider.gameObject.CompareTag("EnemyBase"))
            {
                /*if (_enemy != null)
                    ResetBattlepillarToAttackState();*/

                _enemy = hit.collider.gameObject;  //problem point

            }

        }
        // will this solve the bug? -- no just adds another bug
        /*if (_enemy == null*//* && _extensions.Count > 1*//*)
        {
            LockCaterpillar(false);
            _canMove = true;
        }*/
    }
}
