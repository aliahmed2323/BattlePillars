using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float _shieldModifier;

    private void Start()
    {
        GetComponentInParent<Caterpillar>()._caterPillarDamageTakenModifier -= _shieldModifier;
    }
}
