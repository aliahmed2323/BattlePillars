using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCaterpillar : Caterpillar
{

    LayerMask _enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        _enemyLayer = (1 << 9); // bit shifts to create a layermask containing players layer
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
        if (collision.gameObject.CompareTag("Player"))
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

        if (collision.gameObject.CompareTag("PlayerBase"))
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
        if (collision.gameObject.CompareTag("PlayerBase"))
        {
            LockCaterpillar(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            LockCaterpillar(false);
        }
    }

    void CheckForEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * _dir, Mathf.Infinity, ~_enemyLayer); //~_playerLayer means all layers except _playerLayer
        if (hit)
        {
            Debug.Log(gameObject.name + " has hit " + hit.collider.name);
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                /*if (_enemy != null)
                    ResetBattlepillarToAttackState();*/
                if (enemy != null)
                    _enemy.GetComponent<CaterpillarHealthManager>().onDeath -= ResetBattlepillarToAttackState;

                base._isEnemyInRange = true;
                _enemy = hit.collider.gameObject;  //problem point
                _enemy.GetComponent<CaterpillarHealthManager>().onDeath += ResetBattlepillarToAttackState;

            }

            if (hit.collider.gameObject.CompareTag("PlayerBase"))
            {
              /*  if (_enemy != null)
                    ResetBattlepillarToAttackState();*/

                _enemy = hit.collider.gameObject;  //problem point

            }
        }
    }
}
