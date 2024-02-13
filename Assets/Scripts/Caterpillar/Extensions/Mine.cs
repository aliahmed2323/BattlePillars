using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] GameObject _minePrefab;
    [Tooltip("Drop mine every x seconds")]
    [SerializeField] float _dropRate;

    private void Start()
    {
        Invoke(nameof(DropMine), _dropRate);
    }

    void DropMine()
    {
        if (GetComponentInParent<PlayerCaterpillar>()._canMove)
            Instantiate(_minePrefab, transform.position, Quaternion.identity);
        Invoke(nameof(DropMine), _dropRate);
    }
}
