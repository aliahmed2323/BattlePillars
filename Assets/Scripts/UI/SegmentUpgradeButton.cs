using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentUpgradeButton : MonoBehaviour
{
    [TextArea]
    [SerializeField] string _segmentInfo;


    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => HighlightSegment());
    }

    void HighlightSegment()
    {
        GetComponentInParent<SegmentUpgradeScreen>()._textbox.GetComponent<Text>().text = _segmentInfo;
    }
}
