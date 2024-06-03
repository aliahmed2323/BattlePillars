using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LeafManager : Singleton<LeafManager>
{
    [Tooltip("Time it takes for leafs bar to be full")]
    [HideInInspector]
    public LeafLevels _leafLevel = LeafLevels.Level1;
    [SerializeField] List<LeafUpgrades> _leafUpgrades = new();
    float timeUntilLeafAdd;

    public float _leafGenMultiplier = 1;

    [HideInInspector]
    public LeafUpgrades _currentLeafLevelData;

    public enum LeafLevels
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    }

    [System.Serializable]
    public struct LeafUpgrades
    {
        public LeafLevels _leafLevel;
        public int _maxLeafs;
        public int _timeToFullLeafs;
        public int _upgradeCost;
    }

    private void Awake()
    {
        base.Awake();
        _currentLeafLevelData = GetLeafLevelData(LeafLevels.Level1);
    }

    private void Start()
    {
        timeUntilLeafAdd = (float)_currentLeafLevelData._timeToFullLeafs / (float)_currentLeafLevelData._maxLeafs;
        AddLeafs();
    }

    void AddLeafs()
    {
        if (GameManager.Instance.GetLeafs() >= _currentLeafLevelData._maxLeafs)
            return;

            GameManager.Instance.AddLeafs((int)(1 * _leafGenMultiplier));
        Invoke("AddLeafs", timeUntilLeafAdd);
    }

    // Figures out which level is currently selected and sets it to the next one
    // If leaf level is already level 5, it returns
    public void UpgradeLeaf()
    {
        if (_leafLevel == LeafLevels.Level5) return;

        if (!GameManager.Instance.DeductLeafs(_currentLeafLevelData._upgradeCost))
            return;

        _leafLevel = Enum.GetValues(typeof(LeafLevels)).Cast<LeafLevels>()
        .SkipWhile(e => e != _leafLevel).Skip(1).First();

        _currentLeafLevelData = GetLeafLevelData(_leafLevel);
        timeUntilLeafAdd = (float)_currentLeafLevelData._timeToFullLeafs / (float)_currentLeafLevelData._maxLeafs;
    }

    LeafUpgrades GetLeafLevelData(LeafLevels level)
    {
        foreach(LeafUpgrades l in _leafUpgrades)
        {
            if (l._leafLevel == level)
                return l;
        }
        return _leafUpgrades[0];
    }

}
