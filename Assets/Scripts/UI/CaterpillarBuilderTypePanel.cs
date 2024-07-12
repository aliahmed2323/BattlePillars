using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaterpillarBuilderTypePanel : MonoBehaviour
{
    public GameManager.SegmentType _type;
    public int level;
    int cost;

    private void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(() => AddInBuilder());
        level = SaveManager.Instance._saveData.OwnedSegmentLevel(_type);
        cost = GameManager.Instance._caterPillars[GameManager.Instance._caterPillarType].GetCaterpillarExtension(_type, 1)._leafCost;
    }

    void AddInBuilder()
    {
        if (GameManager.Instance.GetCurrentCaterpillarLength() >= 10) return;
        /*if (GetComponentInParent<CaterpillarBuilderPanel>()._currentBattlepillarCost + cost > LeafManager.Instance._currentLeafLevelData._maxLeafs) return;*/
        if (GetComponentInParent<CaterpillarBuilderPanel>()._currentBattlepillarCost >= LeafManager.Instance._currentLeafLevelData._maxLeafs) return;
        /*if (!GameManager.Instance.DeductLeafs(cost)) return;*/

        GetComponentInParent<CaterpillarBuilderPanel>()._currentBattlepillarCost += cost;
        GetComponentInParent<CaterpillarBuilderPanel>().AddExtension(_type, level);
        GameManager.Instance.AddExtensionToCurrentCaterpillar(_type, level);
    }
}
