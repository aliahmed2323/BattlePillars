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

    private void Start()
    {
        _playButton.onClick.AddListener(() => StartGame());
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
        SceneManager.LoadScene("Singplayer");
    }

}
