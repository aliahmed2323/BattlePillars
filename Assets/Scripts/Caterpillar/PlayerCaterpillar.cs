using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaterpillar : Caterpillar
{
    private void Start()
    {
        _extensions.Add(transform.GetChild(0).gameObject);
        _rayDirecton = Vector2.right;
    }

    private void Move()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, new Vector2(transform.position.x + 0.5f, transform.position.y), Time.deltaTime * _caterPillarSpeed);
        transform.position = newPos;
    }

    private void Update()
    {
        if (_canMove)
            Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _enemy = collision.gameObject;
            _canMove = false;
            collision.gameObject.GetComponent<CaterpillarHealthManager>()._onDeath += ReleaseCaterPillar;
            InvokeStopAnim();
        }
    }

}
