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
    
    public int kills;

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
        Pistolier = 3,
        HealthSnail = 4,
        Rocketier = 5,
        Caraspace = 6,
        Mine = 7,
        Honey = 8,
        Grenadier = 9,
        Feet = 10,
        Bellows = 11
    }

    public enum BaseUpgrades
    {
        Attack,
        Defense,
        Production
    }
    public enum Powerup
    {
        MrLeaf,
        LarryTheBird,
        WizardTurtle
    }

    public enum Boss
    {
        SgtFlowerPuff,
        LtTikkiTakka,
        Frankenpillar,
        GeraldineTheScrapQueen
    }

    private void Start()
    {
        Timer.Instance.StartTimer();
        CreateCaterpillar(0);
    }

    void CreateCaterpillar(int id)
    {
        int randomSpawn = Random.Range(0, _spawnPoints.Count - 1);
        _currentCaterpillar = Instantiate(_caterPillars[id]._headPrefab, _spawnPoints[randomSpawn].position, Quaternion.identity);
        /*_currentCaterpillar.SetActive(false);*/
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
        if (!DeductLeafs(UIManager.Instance._caterPillarBuilderPanel._currentBattlepillarCost)) return;

        /*_currentCaterpillar.SetActive(true);*/
        _currentCaterpillar.GetComponent<Caterpillar>().ReleaseCaterPillar();
        Transform t = UIManager.Instance._caterPillarBuilderPanel._caterPillarPanelHead.transform.parent;
        UIManager.Instance._caterPillarBuilderPanel._currentBattlepillarCost = 0;
        UIManager.Instance._caterPillarBuilderPanel.ResetProgressBar();
        foreach (Transform child in t)
        {
            if (child.name != "Head")
                Destroy(child.gameObject);
        }
        CreateCaterpillar(0);
    }

    public void AddExtensionToCurrentCaterpillar(GameManager.SegmentType type, int level)
    {
        _currentCaterpillar.GetComponent<Caterpillar>().AddExtension(type, false, level);
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
    public void AddLeafs(int amount)
    {
        _leafs += amount;
    }
}
