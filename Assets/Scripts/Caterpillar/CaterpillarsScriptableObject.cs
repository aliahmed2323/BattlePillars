using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CaterpillerData", menuName = "Battlepillars/CreateCaterPillarScriptableObject", order = 1)]
public class CaterpillarsScriptableObject : ScriptableObject
{
    [Header("Caterpiller")]
    public int _id;
    public string _name;
    public Sprite _headImg;
    public GameObject _headPrefab;

    [Header("Extensions")]
    public Extension _canon;
    public Extension _healer;
    public Extension _spikeArmor;
    public Extension _pistolier;
    public Extension _rocketier;
    public Extension _caraspace;
    public Extension _mine;


    [System.Serializable]
    public struct Extension
    {
        public string _name;
        public Sprite _img;
        public GameObject prefab;
        public int _leafCost;
        public Sprite _icon;
    }

    public Extension GetCaterpillarExtension(GameManager.SegmentType type)
    {
        switch(type)
        {
            case GameManager.SegmentType.Cannon:
                return _canon;
            case GameManager.SegmentType.HealthSnail:
                return _healer;
            case GameManager.SegmentType.SpikeyArmor:
                return _spikeArmor;
            case GameManager.SegmentType.Pistolier:
                return _pistolier;
            case GameManager.SegmentType.Rocketier:
                return _rocketier;
            case GameManager.SegmentType.Caraspace:
                return _caraspace;
            case GameManager.SegmentType.Mine:
                return _mine;
            default: 
                return _canon;
        }
    }

}
