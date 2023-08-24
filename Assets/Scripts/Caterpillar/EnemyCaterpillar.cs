using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCaterpillar : Caterpillar
{
    // Start is called before the first frame update
    void Start()
    {
         _extensions.Add(transform.GetChild(0).gameObject);
        _rayDirecton = Vector2.left;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private void Move()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 0.5f, transform.position.y), Time.deltaTime * _caterPillarSpeed);
        transform.position = newPos;
    }

    private void Update()
    {
        if (_canMove)
            Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemy = collision.gameObject;
            _canMove = false;
            InvokeStopAnim();
        }
    }

}
