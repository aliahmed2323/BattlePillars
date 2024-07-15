using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    public DestroyType _destroyType;
    public bool _destroyAfterTime = false;
    public float _timeToDestroySelf;
  public enum DestroyType
    {
        Player,
        Enemy
    }

    private void Start()
    {
        if (_destroyAfterTime)
            Destroy(gameObject, _timeToDestroySelf);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_destroyAfterTime)
            return;

            if (collision.CompareTag("Player") && _destroyType == DestroyType.Player)
                Destroy(gameObject);

            if (collision.CompareTag("Enemy") && _destroyType == DestroyType.Enemy)
                Destroy(gameObject);
    }
}
