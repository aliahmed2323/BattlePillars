using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCaterpillar : Caterpillar
{

    LayerMask _enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        _enemyLayer = (1 << 8); // bit shifts to create a layermask containing players layer
        _extensions.Add(transform.GetChild(0).gameObject);
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
    protected override void Update()
    {
        base.Update();
        if (_enemy == null)
            CheckForEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemy = collision.gameObject;
            _canMove = false;
            InvokeStopAnim();
            LockCaterpillar(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            LockCaterpillar(false);
    }

    void CheckForEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * _dir, Mathf.Infinity, ~_enemyLayer); //~_playerLayer means all layers except _playerLayer
        if (hit)
        {
            Debug.Log(gameObject.name + " has hit " + hit.collider.name);
            if(hit.collider.gameObject.CompareTag("Player"))
            _enemy = hit.collider.gameObject;
        }
    }
}
