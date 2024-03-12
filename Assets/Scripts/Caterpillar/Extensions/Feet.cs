using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    [Tooltip("0 for no boost, 5 for 2x boost")]
    [SerializeField] float _moveSpeedBoost;

    private void Start()
    {
        GetComponentInParent<Caterpillar>()._caterPillarSpeed += _moveSpeedBoost;
    }
}
