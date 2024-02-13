using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burr : MonoBehaviour
{
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] float damage = 0.3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            Explode(collision.gameObject);
    }

    void Explode(GameObject enemy)
    {
        GameObject e = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        e.GetComponent<Animator>().Play("Explosion");
        Destroy(e, 1.1f);
        enemy.GetComponent<CaterpillarHealthManager>()?.DecreaseHealth(damage);
        Destroy(gameObject);
    }
}
