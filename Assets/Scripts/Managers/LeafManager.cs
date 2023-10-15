using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LeafManager : MonoBehaviour
{
    [Tooltip("Time it takes for leafs bar to be full")]
    [SerializeField] int _leafFullTime = 30;
    LeafLevels _leafLevel = LeafLevels.Level1;
    float timeUntilLeafAdd;

    public enum LeafLevels
    {
        Level1 = 220,
        Level2 = 350,
        Level4 = 520,
        Level5 = 600
    }

    private void Start()
    {
        AddLeafs();
        timeUntilLeafAdd = _leafFullTime / ((int)_leafLevel);
    }

    void AddLeafs()
    {
        GameManager.Instance.SetLeafs(1);
        Invoke("AddLeafs", timeUntilLeafAdd);
    }

    // Figures out which level is currently selected and sets it to the next one
    // If leaf level is already level 5, it returns
    public void UpgradeLeaf()
    {
        if (_leafLevel == LeafLevels.Level5) return;

        _leafLevel = Enum.GetValues(typeof(LeafLevels)).Cast<LeafLevels>()
        .SkipWhile(e => e != _leafLevel).Skip(1).First();

        timeUntilLeafAdd = _leafFullTime / ((int)_leafLevel);
    }

}
