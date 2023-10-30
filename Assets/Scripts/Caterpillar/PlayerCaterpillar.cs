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
           if(_enemy == null)
              CheckForEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBase"))
        {
            _enemy = collision.gameObject;
            _canMove = false;
            InvokeStopAnim();
            LockCaterpillar(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBase"))
            LockCaterpillar(false);
    }

    void CheckForEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * _dir, Mathf.Infinity, ~_playerLayer); //~_playerLayer means all layers except _playerLayer
        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("EnemyBase"))
                _enemy = hit.collider.gameObject;
        }
    }
}
