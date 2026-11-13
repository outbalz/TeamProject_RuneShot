using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDetect : MonoBehaviour
{
    #region 인스펙터
    [SerializeField] private LayerMask _MonsterRangelayer;
    [SerializeField] private CircleCollider2D _MonsterRangelayerCollider;
    [SerializeField] private CEnemy _parentEnemy;
    #endregion

    #region 내부변수
    private string _playerTag = "Player";
    private string _playerLayer = "Player";
    private string _wallLayer = "Wall";
    private RaycastHit2D _hit2D;
    private bool _isDetected;
    #endregion

    private void Awake()
    {
        if(_MonsterRangelayerCollider == null || _parentEnemy == null)
        {
            Debug.Log("인스펙터 확인");
        }
          
       _MonsterRangelayer = 1 << LayerMask.NameToLayer(_playerLayer) | 1 << LayerMask.NameToLayer(_wallLayer);
        _MonsterRangelayerCollider.radius = _parentEnemy.Enemydata.DetectionDistance;
    }
     

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.tag == _playerTag)
        {
            _isDetected = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(_parentEnemy.PlayerTr != null)
        {
            return;
        }

        if (!_isDetected)
        {
            return;
        }
        
        Vector3 origin = transform.parent.position;
        Vector3 dir = collision.transform.position - transform.parent.position;
        _hit2D = Physics2D.Raycast(origin, dir, _MonsterRangelayerCollider.bounds.size.x, _MonsterRangelayer);

        if (_hit2D.collider.tag == _playerTag)
        { 
            _parentEnemy.PlayerTr = _hit2D.collider.transform;
            Debug.Log("감지됨");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == _playerTag)
        {
            _isDetected = false;
            _parentEnemy.PlayerTr = null;
            Debug.Log("감지 거리에서 나감");
        }
    }
     
}
