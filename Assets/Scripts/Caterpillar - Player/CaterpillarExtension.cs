using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CaterpillarExtension : MonoBehaviour
{
    public bool animUp;

    private void Start()
    {
        GetComponentInParent<Caterpillar>()._moveAnim += MoveAnim;
    }

    void MoveAnim()
    {
        if (animUp)
            transform.DOLocalMoveY(0.7f, 0.25f).SetLoops(-1, LoopType.Yoyo);
        else
            transform.DOLocalMoveY(0.7f, 0.25f).SetDelay(0.4f).SetLoops(-1, LoopType.Yoyo);
    }
}
