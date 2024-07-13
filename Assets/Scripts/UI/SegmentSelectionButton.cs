using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentSelectionButton : MonoBehaviour
{
    [SerializeField] internal GameManager.SegmentType _segmentType;
    [TextArea]
    [SerializeField] string _segmentInfo;
    bool isSelected;


     void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => HighlightSegment());
    }

    void HighlightSegment()
    {

        if(isSelected)
        {
            GetComponent<Image>().color = new Color32(255,255,255,255);
            isSelected = false;
        }
        else
        {
        if (GetComponentInParent<SegmentSelection>()._selectedSegments.Count >= 6)
            return;
            GetComponent<Image>().color = new Color32(149, 253, 153, 255);
            isSelected = true;
        }
        GetComponentInParent<SegmentSelection>().SelectSegment(_segmentType);
        GetComponentInParent<SegmentSelection>()._segmentInfo.text = _segmentInfo;

    }
}
