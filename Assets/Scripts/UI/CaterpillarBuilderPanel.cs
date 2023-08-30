using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CaterpillarBuilderPanel : MonoBehaviour
{
    [Header("UI Refs")]
    public GameObject _caterPillarPanelHead;
    [SerializeField] Text _leafCountText;
    private void Start()
    {
        UIManager.Instance._caterPillarBuilderPanel = this;
    }

    private void Update()
    {
        _leafCountText.text = GameManager.Instance.GetLeafs().ToString() + "/ 250 Leafs";
    }

    public void AddExtension(int id)
    {
        GameObject ext = Instantiate(_caterPillarPanelHead, _caterPillarPanelHead.transform.parent);
        Sprite img = GameManager.Instance._caterPillars[GameManager.Instance._caterPillarType].GetCaterpillarExtension(id)._img;
        ext.GetComponent<Image>().sprite = img;
    }
}
