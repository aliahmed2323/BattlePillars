using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoneyObject : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void StartFading(float timeToCompletion, float statsResetAmount)
    {
        GetComponent<SpriteRenderer>().DOFade(0, timeToCompletion).OnComplete(() =>
        {
            GetComponentInParent<Caterpillar>()._caterPillarSpeed += statsResetAmount;
            Destroy(gameObject);
        });
    }
}
