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

    [SerializeField] Transform _enemySpawns;

    [SerializeField] Levels[] _levels;

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
        StartCoroutine(SpawnPillars());
    }

    IEnumerator SpawnPillars()
    {
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
