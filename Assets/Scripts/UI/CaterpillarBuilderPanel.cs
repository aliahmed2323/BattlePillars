using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CaterpillarBuilderPanel : MonoBehaviour
{
    [Header("UI Refs")]
    public GameObject _caterPillarPanelHead;
    [SerializeField] Text _leafCountText;
    [SerializeField] GameObject _extensionTypePanel;
    private void Start()
    {
        UIManager.Instance._caterPillarBuilderPanel = this;
        AddSegmentsToScrollView();
    }

    private void Update()
    {
        _leafCountText.text = GameManager.Instance.GetLeafs().ToString() + "/ 250 Leafs";
    }

    public void AddExtension(GameManager.SegmentType type)
    {
        GameObject ext = Instantiate(_caterPillarPanelHead, _caterPillarPanelHead.transform.parent);
        Sprite img = GameManager.Instance._caterPillars[GameManager.Instance._caterPillarType].GetCaterpillarExtension(type)._img;
        ext.GetComponent<Image>().sprite = img;
    }

     void AddSegmentsToScrollView()
    {
        Transform content = GetComponentInChildren<ScrollRect>().content.transform;
        string[] segmentinfo = PlayerPrefs.GetString("SegmentInfo").Split(":");
        foreach(string id in segmentinfo)
        {
            GameManager.SegmentType segment = (GameManager.SegmentType)Enum.ToObject(typeof(GameManager.SegmentType), Convert.ToInt32(id));
            Debug.Log(segment.ToString());
            GameObject item = Instantiate(_extensionTypePanel, content);
            item.GetComponent<CaterpillarBuilderTypePanel>()._type = segment;
            item.GetComponent<Image>().sprite = GameManager.Instance._caterPillars[GameManager.Instance._caterPillarType].GetCaterpillarExtension(segment)._icon;
        }
    }
}
