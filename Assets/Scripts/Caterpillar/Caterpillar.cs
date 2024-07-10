using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caterpillar : MonoBehaviour
{
    public List<GameObject> _extensions = new();

    [Header("Caterpillar Modifiers")]
    public float _caterPillarSpeed = 5;
    public float _caterPillarHealth = 1;
    public float _caterPillarDamageModifier = 1;
    public float _caterPillarDamageTakenModifier = 1;

    [Tooltip("1 for right and -1 for left")]
    public float _dir;

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

    private bool _lockCaterpillarMovement;


/*    private GameObject m_enemy = null;
    public GameObject _enemy
    {
        get { return m_enemy; }
        set
        {
            if (m_enemy == _enemy) return;
            m_enemy = _enemy;
            if(_enemy != null)
            {
                ResetBattlepillarToAttackState();

            }
        }
    }*/
    /*    _enemy.GetComponent<CaterpillarHealthManager>().onDeath += ResetBattlepillarToAttackState
     *    public delegate void OnEnemyChangeDelegate();
        public event OnEnemyChangeDelegate OnEnemyChange;

        public void ResetBattlepillarToAttackState()
        {
            if (_lockCaterpillarMovement) return; //return if movement locked
            InvokeStopAnim();
            _moveAnim.Invoke();
            _canMove = true;
        }*/

    public void ResetBattlepillarToAttackState()
    {
        _lockCaterpillarMovement = false;
        _enemy = null;
        _canMove = true;
        _moveAnim.Invoke();
    }

    private void Move()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, new Vector2(transform.position.x + 0.5f * _dir, transform.position.y), Time.deltaTime * _caterPillarSpeed);
        transform.position = newPos;
    }

    protected virtual void Update()
    {
        if (_canMove)
            Move();
    }

    public void AddExtension(GameManager.SegmentType type, bool isEnemy, int level)
    {
        Vector2 newPos = new Vector2(_extensions[_extensions.Count - 1].transform.position.x - _extensionGap, transform.position.y);
        CaterpillarsScriptableObject.Extension extension;

        // Currently dont have some of these segments for enemy so resetting them to cannon incase this instance is an enemy
        GameManager.SegmentType adjustedType;
        if (type == GameManager.SegmentType.Caraspace || type == GameManager.SegmentType.Mine || type == GameManager.SegmentType.Rocketier || type == GameManager.SegmentType.Pistolier)
            adjustedType = GameManager.SegmentType.Cannon;
        else
            adjustedType = type;

        if (!isEnemy)
        extension = GameManager.Instance._caterPillars[GameManager.Instance._caterPillarType].GetCaterpillarExtension(type, level);
        else
             extension = GameManager.Instance._enemyCaterPillars[0].GetCaterpillarExtension(adjustedType, level);

        GameObject ext = Instantiate(extension.prefab, newPos, Quaternion.identity, transform);
        _extensions.Add(ext);
        if (_extensions.Count % 2 == 0)
            ext.GetComponent<CaterpillarExtension>().animUp = true;
    }

    public void ReleaseCaterPillar()
    {
        if (_lockCaterpillarMovement) return; //return if movement locked
        _moveAnim.Invoke();
        _canMove = true;
    }

    public void InvokeStopAnim()
    {
        _stopMoveAnim.Invoke(); 
    }

    public void LockCaterpillar(bool state)
    {
        _lockCaterpillarMovement = state;
    }
}
