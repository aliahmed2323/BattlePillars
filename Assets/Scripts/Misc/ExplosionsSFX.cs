using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsSFX : MonoBehaviour
{
    [SerializeField] AudioSource _as;

    private void Start()
    {
        _as.Play();
    }
}
