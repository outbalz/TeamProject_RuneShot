using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDetect : MonoBehaviour
{

    private string _playerTag = "Player";
    private string _playerLayer = "Player";
    private string _wallLayer = "Wall";

    private LayerMask _MonsterRangelayer;
    private Collider2D _MonsterRangelayerCollider;

    private RaycastHit2D _hit2D;

    private bool _isDetected;

    CEnemy _parentEnemy;
    private void Awake()
    {
        _MonsterRangelayerCollider = GetComponent<Collider2D>();
        _MonsterRangelayer = 1 << LayerMask.NameToLayer(_playerLayer) | 1 << LayerMask.NameToLayer(_wallLayer);

        _parentEnemy = transform.parent.GetComponent<CEnemy>();
    }
     

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("감지됨");
        if (collision.tag == _playerTag)
        {
            //Enemy에 감지된 정보를 보낸다
            Vector3 origin = transform.parent.position;
            Vector3 dir = collision.transform.position - transform.parent.position;
            _hit2D = Physics2D.Raycast(origin, dir, _MonsterRangelayerCollider.bounds.size.x, _MonsterRangelayer);

            if (_hit2D.collider.tag == _playerTag)
            {
                _isDetected = true;
                _parentEnemy.PlayerTr = _hit2D.collider.transform;
                Debug.Log("감지됨");
            }
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
