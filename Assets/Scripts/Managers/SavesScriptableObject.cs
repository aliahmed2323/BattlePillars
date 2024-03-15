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

    public CompletedLevels GetCompletedLevel(int _levelID)
    {
        foreach(CompletedLevels l in _playerData._completedLevels)
        {
            if (l._levelID == _levelID)
                return l;
        }
        return _playerData._completedLevels[0];
    }
}
