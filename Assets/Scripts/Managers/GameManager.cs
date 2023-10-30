using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public bool _debugRays;

    [SerializeField] int _leafs;

    [Header("Caterpillar Refs")]
    public List<CaterpillarsScriptableObject> _caterPillars = new();
    public List<CaterpillarsScriptableObject> _enemyCaterPillars = new();
    public int _caterPillarType;

    [Header("Gameplay refs")]
    [SerializeField] List<Transform> _spawnPoints = new();

    //temp
    public Transform enemyspawn;
    public GameObject enemycaterpillar;

    //Functionality vars
    GameObject _currentCaterpillar;


    // Segment Types
    public enum SegmentType
    {
        SpikeyArmor = 0,
        Cannon = 1,
        Grenadier = 2,
        Pistolier = 3,
        HealthSnail = 4,
        Rocketier = 5
    }

    private void Start()
    {
        CreateCaterpillar(0);
    }

    void CreateCaterpillar(int id)
    {
        int randomSpawn = Random.Range(0, _spawnPoints.Count - 1);
        _currentCaterpillar = Instantiate(_caterPillars[id]._headPrefab, _spawnPoints[randomSpawn].position, Quaternion.identity);
    }

    /*//temp
    IEnumerator InstantiateEnemyCaterpillar()
    {
        GameObject ct = Instantiate(enemycaterpillar, enemyspawn.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        ct.GetComponent<Caterpillar>().AddExtension(SegmentType.Cannon, true);
        ct.GetComponent<Caterpillar>().AddExtension(SegmentType.Cannon, true);
        ct.GetComponent<Caterpillar>().AddExtension(SegmentType.Cannon, true);
        yield return new WaitForSeconds(0.1f);
        ct.GetComponent<Caterpillar>().ReleaseCaterPillar();

        Invoke("instantenemcat", 20f);
    }
    void instantenemcat()
    {
        StartCoroutine(InstantiateEnemyCaterpillar());
    }
    // temp end*/

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

    public void AddExtensionToCurrentCaterpillar(GameManager.SegmentType type)
    {
        _currentCaterpillar.GetComponent<Caterpillar>().AddExtension(type, false);
    }
    public int GetCurrentCaterpillarLength()
    {
        return _currentCaterpillar.GetComponent<Caterpillar>()._extensions.Count;
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
    public void SetLeafs(int amount)
    {
        _leafs = amount;
    }
}
