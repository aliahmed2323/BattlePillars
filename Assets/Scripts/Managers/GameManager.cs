using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] int _leafs;

    [Header("Caterpillar Refs")]
    public List<CaterpillarsScriptableObject> _caterPillars = new();
    public int _caterPillarType;

    [Header("Gameplay refs")]
    [SerializeField] List<Transform> _spawnPoints = new();

    //temp
    [SerializeField] Transform enemyspawn;
    [SerializeField] GameObject enemycaterpillar;

    //Functionality vars
    GameObject _currentCaterpillar;

    private void Start()
    {
        CreateCaterpillar(0);
        StartCoroutine(InstantiateEnemyCaterpillar());
    }

    void CreateCaterpillar(int id)
    {
        int randomSpawn = Random.Range(0, _spawnPoints.Count - 1);
        _currentCaterpillar = Instantiate(_caterPillars[id]._headPrefab, _spawnPoints[randomSpawn].position, Quaternion.identity);
    }

    //temp
    IEnumerator InstantiateEnemyCaterpillar()
    {
        GameObject ct = Instantiate(enemycaterpillar, enemyspawn.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        ct.GetComponent<Caterpillar>().AddExtension(0);
        ct.GetComponent<Caterpillar>().AddExtension(1);
        ct.GetComponent<Caterpillar>().AddExtension(2);
        ct.GetComponent<Caterpillar>().ReleaseCaterPillar();
    }

    public void ReleaseCaterpillar()
    {
        _currentCaterpillar.GetComponent<Caterpillar>().ReleaseCaterPillar();
        Transform t = UIManager.Instance._caterPillarBuilderPanel._caterPillarPanelHead.transform.parent;
        foreach (Transform child in t)
        {
            if (child.name != "Head")
                Destroy(child.gameObject);
        }
        CreateCaterpillar(0);
    }

    public void AddExtensionToCurrentCaterpillar(int type)
    {
        _currentCaterpillar.GetComponent<Caterpillar>().AddExtension(type);
    }

    public bool DeductLeafs(int amount)
    {
        if (_leafs >= amount)
        {
            _leafs -= amount;
            return true;
        }
        return false;
    }
    public int GetLeafs()
    {
        return _leafs;
    }
}
