using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;  

public class SegmentSelection : MonoBehaviour
{
    [SerializeField] List<GameManager.SegmentType> _selectedSegments;
    public Text _segmentInfo;
    [SerializeField] Button _playButton;

    private void Start()
    {
        _playButton.onClick.AddListener(() => StartGame());
    }


    public void SelectSegment(GameManager.SegmentType segmentType)
    {
        if (_selectedSegments.Contains(segmentType))
            RemoveSegment(segmentType);
        else
            AddSegment(segmentType);
    }

    void RemoveSegment(GameManager.SegmentType segment)
    {
        _selectedSegments.Remove(segment);
    }
    void AddSegment(GameManager.SegmentType segment)
    {
        _selectedSegments.Add(segment);
    }

    void StartGame()
    {
        string segmentinfo = "";
        _selectedSegments.ForEach(delegate(GameManager.SegmentType item)
        {
            segmentinfo = segmentinfo + ((int)item).ToString() +":";
        });
        PlayerPrefs.SetString("SegmentInfo", segmentinfo);
        SceneManager.LoadScene("Singplayer");
    }

}
