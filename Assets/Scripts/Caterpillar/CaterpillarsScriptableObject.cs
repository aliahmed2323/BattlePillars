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
    public Extension[] _canon;
    public Extension[] _healer;
    public Extension[] _spikeArmor;
    public Extension[] _pistolier;
    public Extension[] _rocketier;
    public Extension[] _caraspace;
    public Extension[] _mine;
    public Extension[] _honey;
    public Extension[] _grenadier;
    public Extension[] _feet;
    public Extension[] _bellows;


    [System.Serializable]
    public struct Extension
    {
        public string _name;
        public Sprite _img;
        public GameObject prefab;
        public int _leafCost;
        public int _upgradeCost;
        public Sprite _icon;
    }

    public Extension GetCaterpillarExtension(GameManager.SegmentType type, int level)
    {
        switch(type)
        {
            case GameManager.SegmentType.Cannon:
                return _canon[level];
            case GameManager.SegmentType.HealthSnail:
                return _healer[level];
            case GameManager.SegmentType.SpikeyArmor:
                return _spikeArmor[level];
            case GameManager.SegmentType.Pistolier:
                return _pistolier[level];
            case GameManager.SegmentType.Rocketier:
                return _rocketier[level];
            case GameManager.SegmentType.Caraspace:
                return _caraspace[level];
            case GameManager.SegmentType.Mine:
                return _mine[level];
            case GameManager.SegmentType.Honey:
                return _honey[level];
            case GameManager.SegmentType.Feet:
                return _feet[level];
            case GameManager.SegmentType.Grenadier:
                return _grenadier[level];
            case GameManager.SegmentType.Bellows:
                return _bellows[level];
            default: 
                return _canon[level];
        }
    }

}
