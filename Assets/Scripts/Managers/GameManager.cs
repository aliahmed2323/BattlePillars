using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Caterpillar Refs")]
    public  List<CaterpillarsScriptableObject> _caterPillars = new();
    public int _caterPillarType;

    [Header("Gameplay refs")]
    [SerializeField] List<Transform> _spawnPoints = new();

    //Functionality vars
    GameObject _currentCaterpillar;

    private void Start()
    {
        CreateCaterpillar(0);
    }

    void CreateCaterpillar(int id)
    {
        int randomSpawn = Random.Range(0, _spawnPoints.Count - 1);
        _currentCaterpillar = Instantiate(_caterPillars[id]._headPrefab, _spawnPoints[randomSpawn].position, Quaternion.identity);
    }

    public void ReleaseCaterpillar()
    {
        _currentCaterpillar.GetComponent<Caterpillar>().ReleaseCaterPillar();
        CreateCaterpillar(0);
    }

    public void AddExtensionToCurrentCaterpillar(int type)
    {
        _currentCaterpillar.GetComponent<Caterpillar>().AddExtension(type);
    }

}
