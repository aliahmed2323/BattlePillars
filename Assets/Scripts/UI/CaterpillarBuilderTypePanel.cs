using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaterpillarBuilderTypePanel : MonoBehaviour
{
    public GameManager.SegmentType _type;
    int cost;

    private void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(() => AddInBuilder());
        cost = GameManager.Instance._caterPillars[GameManager.Instance._caterPillarType].GetCaterpillarExtension(_type)._leafCost;
    }

    void AddInBuilder()
    {
        if (GameManager.Instance.GetCurrentCaterpillarLength() >= 10) return;
            if (!GameManager.Instance.DeductLeafs(cost)) return;
        GetComponentInParent<CaterpillarBuilderPanel>().AddExtension(_type);
        GameManager.Instance.AddExtensionToCurrentCaterpillar(_type);
    }
}
