using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;  

public class SegmentSelection : MonoBehaviour
{
    [SerializeField] internal List<GameManager.SegmentType> _selectedSegments;
    [SerializeField] List<Image> _selectedSegmentImages;
    [SerializeField] Sprite _defaultImage;
    public Text _segmentInfo;
    [SerializeField] Button _playButton;
    [SerializeField] GameObject[] _segmentScrollViewObjects;
    [HideInInspector]
    public bool _isEndless;

    private void Start()
    {
        _playButton.onClick.AddListener(() => StartGame());
        UpdateOwnedSegmentsInScrollView();
    }

    private void UpdateSegmentImages()
    {
        for(int i = 0; i < _selectedSegmentImages.Count; i++)
        {
            if(_selectedSegments.Count > i)
                _selectedSegmentImages[i].sprite = UIManager.Instance._caterpillar.GetCaterpillarExtension(_selectedSegments[i], SaveManager.Instance._saveData.OwnedSegmentLevel(_selectedSegments[i]))._icon;
            else
            {
                _selectedSegmentImages[i].sprite = _defaultImage;
            }
            
        }
    }

    private void OnEnable()
    {
        UpdateOwnedSegmentsInScrollView();
    }

    public void UpdateOwnedSegmentsInScrollView()
    {
        foreach(GameObject g in _segmentScrollViewObjects)
        {
            if (!SaveManager.Instance._saveData.IsSegmentOwned(g.GetComponent<SegmentSelectionButton>()._segmentType))
                g.SetActive(false);
            else
            {
                g.SetActive(true);
                g.GetComponent<Image>().sprite = UIManager.Instance._caterpillar.GetCaterpillarExtension(g.GetComponent<SegmentSelectionButton>()._segmentType, SaveManager.Instance._saveData.OwnedSegmentLevel(g.GetComponent<SegmentSelectionButton>()._segmentType))._icon;

            }
        }
    }

    public void SelectSegment(GameManager.SegmentType segmentType)
    {
        if (_selectedSegments.Contains(segmentType))
            RemoveSegment(segmentType);
        else
            AddSegment(segmentType);

        UpdateSegmentImages();
    }

    void RemoveSegment(GameManager.SegmentType segment)
    {
        _selectedSegments.Remove(segment);
    }
    void AddSegment(GameManager.SegmentType segment)
    {
        if (_selectedSegments.Count >= 6)
            return;
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
        if(!_isEndless)
            SceneManager.LoadScene("Singplayer");
        else
            SceneManager.LoadScene("Endless");
    }

}
