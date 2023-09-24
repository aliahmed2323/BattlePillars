using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] int _aggressionLevel;
    [SerializeField] int _desperationLevel;
    [SerializeField] int _resourcesAvailable;
    [SerializeField] float _maxCaterpillarSpawns;

    [SerializeField] DifficultySettings _difficultySettings;

    [System.Serializable]
    public struct DifficultySettings
    {
        public int ExtensionLevel;
        public GameManager.ExtensionTypes[] AllowedExtensions;
    }

    void SpawnEnemy()
    {

    }
}
