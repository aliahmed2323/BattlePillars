using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CaterpillarExtension : MonoBehaviour
{
    public bool animUp;
    Tween _moveAnim;

    private void Start()
    {
        GetComponentInParent<Caterpillar>()._moveAnim += MoveAnim;
        GetComponentInParent<Caterpillar>()._stopMoveAnim += StopMoveAnim;
    }

    void MoveAnim()
    {
        _moveAnim.Kill();

        if (animUp)
            _moveAnim = transform.DOLocalMoveY(0.7f, 0.25f).SetLoops(-1, LoopType.Yoyo);
        else
            _moveAnim = transform.DOLocalMoveY(0.7f, 0.25f).SetDelay(0.4f).SetLoops(-1, LoopType.Yoyo);
    }

    void StopMoveAnim()
    {
        _moveAnim.SmoothRewind();
    }
}
