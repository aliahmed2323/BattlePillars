using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAIManager : MonoBehaviour
{
    /*   [SerializeField] List<UsableSegments> _usableSegments = new();
       [SerializeField] float _desperationLevel = 1;
       [SerializeField] float _aggressionLevel = 1;
       [SerializeField] float _resourcesLevel = 1;

       [System.Serializable]
       public struct UsableSegments
       {
           public GameManager.SegmentType _segmentType;
           public int _segmentLevel;
       }*/

    // ALL OF THIS IS TEMP SHIT

    public int _currentLevel;
    bool _isBossBattle = false;

    [SerializeField] Transform _enemySpawns;
    [SerializeField] Transform _bossSpawns;

    [SerializeField] Levels[] _levels;
    [SerializeField] GameObject[] _bossGameObjects;

    [System.Serializable]
    public struct Levels
    {
       public int level;
       public  Pillars[] pillar;
    }


    [System.Serializable]
    public struct Pillars
    {
        public GameManager.SegmentType[] Segments;
    }

    private void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("Level");

        int[] bossLevels = { 10, 20, 30, 40, 50 };

        foreach (int lvl in bossLevels)
            if (_currentLevel == lvl)
                _isBossBattle = true;

        if(!_isBossBattle)
            StartCoroutine(SpawnPillars());

        if (_isBossBattle)
            SpawnBoss(_currentLevel);
    }

    void SpawnBoss(int lvl)
    {
        switch(lvl)
        {
            case 10:
                Instantiate(_bossGameObjects[0], new Vector3(_bossSpawns.transform.position.x, _bossSpawns.transform.position.y, _bossSpawns.transform.position.z), Quaternion.identity);
                break;
            case 20:
                Instantiate(_bossGameObjects[1], new Vector3(_bossSpawns.transform.position.x, _bossSpawns.transform.position.y, _bossSpawns.transform.position.z), Quaternion.identity);
                break;
            case 30:
                Instantiate(_bossGameObjects[2], new Vector3(_bossSpawns.transform.position.x, _bossSpawns.transform.position.y, _bossSpawns.transform.position.z), Quaternion.identity);
                break;
            case 40:
                Instantiate(_bossGameObjects[3], new Vector3(_bossSpawns.transform.position.x, _bossSpawns.transform.position.y, _bossSpawns.transform.position.z), Quaternion.identity);
                break;
        }
    }

    IEnumerator SpawnPillars()
    {
        Debug.Log("Spawning battlepillars for level: " + _currentLevel);
        foreach(Pillars pillar in _levels[_currentLevel-1].pillar)
        {
            int randSpawn = Random.Range(0, 2);
            GameObject ct = Instantiate(GameManager.Instance.enemycaterpillar, _enemySpawns.transform.GetChild(randSpawn).position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            foreach(GameManager.SegmentType type in pillar.Segments)
            {
                ct.GetComponent<Caterpillar>().AddExtension(type, true);
            }
            yield return new WaitForSeconds(0.1f);
            ct.GetComponent<Caterpillar>().ReleaseCaterPillar();
            yield return new WaitForSeconds(15f);
        }
    }
}
