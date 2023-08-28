using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caterpillar : MonoBehaviour
{
    public List<GameObject> _extensions = new();

    [Header("Caterpillar Modifiers")]
    public float _caterPillarSpeed;
    public float _caterPillarHealth;
    public float _caterPillarDamageModifier;
    public float _caterPillarDamageTakenModifier;

    [SerializeField] float _extensionGap;

    [HideInInspector]
    public bool _canMove;

    [HideInInspector]
    public delegate void MoveAnim();
    public event MoveAnim _moveAnim;

    [HideInInspector]
    public delegate void StopMoveAnim();
    public event StopMoveAnim _stopMoveAnim;

    public GameObject _enemy;
    public bool _isEnemyInRange;

    [HideInInspector]
    public Vector2 _rayDirecton;

    private void Start()
    {

    }

    private void Update()
    {
        /*       if (_enemy == null)
                   CheckForEnemy();*/
    }

    public void AddExtension(int type)
    {
        Vector2 newPos = new Vector2(_extensions[_extensions.Count - 1].transform.position.x - _extensionGap, transform.position.y);
        CaterpillarsScriptableObject.Extension extension = GameManager.Instance._caterPillars[GameManager.Instance._caterPillarType].GetCaterpillarExtension(type);
        GameObject ext = Instantiate(extension.prefab, newPos, Quaternion.identity, transform);
        _extensions.Add(ext);
        if (_extensions.Count % 2 == 0)
            ext.GetComponent<CaterpillarExtension>().animUp = true;
    }

    public void ReleaseCaterPillar()
    {
        _moveAnim.Invoke();
        _canMove = true;
    }

    public void InvokeStopAnim()
    {
        _stopMoveAnim.Invoke();
    }
}
