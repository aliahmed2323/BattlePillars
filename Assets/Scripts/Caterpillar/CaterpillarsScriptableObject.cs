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
    public Extension _shield;
    public Extension _spikeArmor;


    [System.Serializable]
    public struct Extension
    {
        public string _name;
        public Sprite _img;
        public GameObject prefab;
        public int _leafCost;
    }

    public Extension GetCaterpillarExtension(int id)
    {
        switch(id)
        {
            case 0:
                return _canon;
            case 1:
                return _healer;
            case 2:
                return _shield;
            case 3:
                return _spikeArmor;
            default: 
                return _canon;
        }
    }

}
