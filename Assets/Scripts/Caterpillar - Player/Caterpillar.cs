using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caterpillar : MonoBehaviour
{
    [SerializeField] List<GameObject> _extensions = new();
    [SerializeField] GameObject[] _extensionTypes;
    [SerializeField] Button btn;
    [SerializeField] float _caterPillarSpeed;

    bool _canMove;

    [HideInInspector]
    public delegate void MoveAnim();
    public event MoveAnim _moveAnim;

    [HideInInspector]
    public delegate void StopMoveAnim();
    public event StopMoveAnim _stopMoveAnim;

    private void Start()
    {
        _extensions.Add(transform.GetChild(0).gameObject);
    }

    private void Update()
    {
        if(_canMove)
        Move();
    }
    private void Move()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, new Vector2(transform.position.x + 0.5f, transform.position.y), Time.deltaTime * _caterPillarSpeed);
        transform.position = newPos;
    }

    public void AddExtension(int type)
    {
        Vector2 newPos = new Vector2(_extensions[_extensions.Count - 1].transform.position.x - 1.7f, transform.position.y);
        CaterpillarsScriptableObject.Extension extension = GameManager.Instance._caterPillars[GameManager.Instance._caterPillarType].GetCaterpillarExtension(type);
        GameObject ext = Instantiate(extension.prefab, newPos, Quaternion.identity, transform);
        _extensions.Add(ext);
        if(_extensions.Count % 2 == 0)
        ext.GetComponent<CaterpillarExtension>().animUp = true;
    }

    public void ReleaseCaterPillar()
    {
        _moveAnim.Invoke();
        _canMove = true;
    }
}
