using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save Data", menuName = "Battlepillars/Create Save Data Scriptable Object", order = 1)]
public class SavesScriptableObject : ScriptableObject
{
    public PlayerData _playerData;

    [System.Serializable]
    public struct PlayerData
    {
        public int _apples;
        public SaveManager.Modes[] _unlockedModes;
        public List<CompletedLevels> _completedLevels;
        public List<OwnedSegments> _ownedSegments;
        public List<OwnedBaseUpgrades> _baseUpgrades;
        public int _ownedMrLeaf;
        public int _ownedLarry;
        public int _ownedWizard;
    }

    [System.Serializable]
    public struct CompletedLevels
    {
        public int _levelID;
        public int _completionTime;
        public int _score;
    }

    [System.Serializable]
    public struct OwnedSegments
    {
        public GameManager.SegmentType _segmentType;
        public int _segmentLevel;
    }

    [System.Serializable]
    public struct OwnedBaseUpgrades
    {
        public GameManager.BaseUpgrades BaseUpgrades;
        public int Level;
    }

    public CompletedLevels GetCompletedLevel(int _levelID)
    {
        foreach (CompletedLevels l in _playerData._completedLevels)
        {
            if (l._levelID == _levelID)
                return l;
        }
        return _playerData._completedLevels[0];
    }

    /*public OwnedSegments GetOwnedSegment()
    {
        foreach()
    }*/

    public bool IsSegmentOwned(GameManager.SegmentType segmentType)
    {
        foreach (OwnedSegments i in _playerData._ownedSegments)
        {
            if (i._segmentType == segmentType)
                return true;
        }
        return false;
    }

    public int OwnedSegmentLevel(GameManager.SegmentType segmentType)
    {
        foreach (OwnedSegments i in _playerData._ownedSegments)
        {
            if (i._segmentType == segmentType)
                return i._segmentLevel;
        }
        return 0;
    }

    public int OwnedBaseUpgradeLevel(GameManager.BaseUpgrades Type)
    {
        foreach (OwnedBaseUpgrades i in _playerData._baseUpgrades)
        {
            if (i.BaseUpgrades == Type)
                return i.Level;
        }
        return 0;
    }

    public bool IsBaseUpgradeOwned(GameManager.BaseUpgrades type)
    {
        foreach (OwnedBaseUpgrades i in _playerData._baseUpgrades)
        {
            if (i.BaseUpgrades == type)
                return true;
        }
        return false;
    }

    public bool RemoveBaseUpgrade(GameManager.BaseUpgrades type)
    {
        for (int i = 0; i < _playerData._baseUpgrades.Count; i++)
        {
            if (_playerData._baseUpgrades[i].BaseUpgrades == type)
            {
                _playerData._baseUpgrades.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public bool RemoveSegment(GameManager.SegmentType type)
    {
        for (int i = 0; i < _playerData._ownedSegments.Count; i++)
        {
            if (_playerData._ownedSegments[i]._segmentType == type)
            {
                _playerData._ownedSegments.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public int GetOwnedPowerup(GameManager.Powerup type)
    {
        switch (type)
        {
            case GameManager.Powerup.MrLeaf:
                return _playerData._ownedMrLeaf;
            case GameManager.Powerup.LarryTheBird:
                return _playerData._ownedLarry;
            case GameManager.Powerup.WizardTurtle:
                return _playerData._ownedWizard;
            default:
                return 0;
        }
    }

    public void AddPowerup(GameManager.Powerup type)
    {
        switch (type)
        {
            case GameManager.Powerup.MrLeaf:
                _playerData._ownedMrLeaf++;
                break;
            case GameManager.Powerup.LarryTheBird:
                _playerData._ownedLarry++;
                break; 
            case GameManager.Powerup.WizardTurtle:
                _playerData._ownedWizard++;
                break;
            default:
                return;
        }
    }

    public void ReducePowerup(GameManager.Powerup type)
    {
        switch (type)
        {
            case GameManager.Powerup.MrLeaf:
                _playerData._ownedMrLeaf--;
                break;
            case GameManager.Powerup.LarryTheBird:
                _playerData._ownedLarry--;
                break;
            case GameManager.Powerup.WizardTurtle:
                _playerData._ownedWizard--;
                break;
            default:
                return;
        }
    }
}
