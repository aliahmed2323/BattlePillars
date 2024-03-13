using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    public DestroyType _destroyType;
  public enum DestroyType
    {
        Player,
        Enemy
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player") && _destroyType == DestroyType.Player)
                Destroy(gameObject);

            if (collision.CompareTag("Enemy") && _destroyType == DestroyType.Enemy)
                Destroy(gameObject);
    }
}
