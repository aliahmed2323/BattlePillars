using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaterpillarBuilderPanel : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] GameObject _caterPillarPanelHead;

    public void AddExtension(Sprite img)
    {
/*        GameObject ext = Instantiate(_caterPillarPanelHead, _caterPillarPanelHead.transform);
        ext.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x - 5, transform.position.y);
        ext.GetComponent<Image>().sprite = img;*/
    }
}
