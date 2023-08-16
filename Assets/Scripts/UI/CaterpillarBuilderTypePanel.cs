using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaterpillarBuilderTypePanel : MonoBehaviour
{
    [SerializeField] int _type;

    private void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(() => GameManager.Instance.AddExtensionToCurrentCaterpillar(_type));
    }

    void AddInBuilder()
    {
        GetComponentInParent<CaterpillarBuilderPanel>().AddExtension(GetComponent<Image>().sprite);
    }
}
