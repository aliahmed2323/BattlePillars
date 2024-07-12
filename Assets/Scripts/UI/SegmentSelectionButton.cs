using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentSelectionButton : MonoBehaviour
{
    [SerializeField] GameManager.SegmentType _segmentType;
    [TextArea]
    [SerializeField] string _segmentInfo;
    bool isSelected;


     void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => HighlightSegment() );
        if (!SaveManager.Instance._saveData.IsSegmentOwned(_segmentType))
            gameObject.SetActive(false);
        else
        {
            GetComponent<Image>().sprite = UIManager.Instance._caterpillar.GetCaterpillarExtension(_segmentType, SaveManager.Instance._saveData.OwnedSegmentLevel(_segmentType))._icon;

        }
    }

    void HighlightSegment()
    {
        GetComponentInParent<SegmentSelection>().SelectSegment(_segmentType);
        GetComponentInParent<SegmentSelection>()._segmentInfo.text = _segmentInfo;
        if(isSelected)
        {
            GetComponent<Image>().color = new Color32(255,255,255,255);
            isSelected = false;
        }
        else
        {
            GetComponent<Image>().color = new Color32(149, 253, 153, 255);
            isSelected = true;
        }

    }
}
