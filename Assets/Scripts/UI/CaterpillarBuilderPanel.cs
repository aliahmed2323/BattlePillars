using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CaterpillarBuilderPanel : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] GameObject _caterPillarPanelHead;

    public void AddExtension(Sprite img)
    {
        GameObject ext = Instantiate(_caterPillarPanelHead, _caterPillarPanelHead.transform);
        ext.transform.DOMoveX(-2, 0f);
        ext.GetComponent<Image>().sprite = img;
    }
}
