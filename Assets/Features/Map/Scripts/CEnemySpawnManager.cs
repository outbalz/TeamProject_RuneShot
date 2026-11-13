using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySpawnManager : MonoBehaviour
{
    [Header("적 설정")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("플레이어 설정")]
    [SerializeField] private Transform _player;

    [Header("스폰 설정")]
    [SerializeField] private float _spawnDistance = 5f;
    [SerializeField] private int _spawnCount = 3;

    [Header("생성 시간")]
    [SerializeField] private float _spawnDelay = 1.5f;
    [SerializeField] private float _spawnInterval = 0.5f;

    [Header("생성 위치")]
    [SerializeField] private float _spawnOffset = 0.5f;

    private bool[] _isSpawning;
    private bool[] _isSpawned;


    private void Start()
    {
        int spawnCount = transform.childCount;

        _isSpawning = new bool[spawnCount];
        _isSpawned = new bool[spawnCount];

        if (_player == null)
        {
            Debug.LogWarning("플레이어 연결 안됨");
        }

        if (_enemyPrefab == null)
        {
            Debug.LogWarning("적 프리팹 연결 안됨");
        }
    }


    private void Update()
    {
        if (_player == null || _enemyPrefab == null)
        {
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (_isSpawning[i] || _isSpawned[i])
            {
                continue;
            }

            Transform spawnPoint = transform.GetChild(i);

            float distance = Vector2.Distance(
                _player.position,
                spawnPoint.position
            );

            if (distance <= _spawnDistance)
            {
                _isSpawning[i] = true;

                StartCoroutine(CoSpawnEnemies(i));
            }
        }
    }


    private IEnumerator CoSpawnEnemies(int spawnIndex)
    {
        yield return new WaitForSeconds(_spawnDelay);

        Transform spawnPoint = transform.GetChild(spawnIndex);

        for (int i = 0; i < _spawnCount; i++)
        {
            Vector2 randomPosition = Random.insideUnitCircle * _spawnOffset;

            Vector3 spawnPosition = spawnPoint.position + new Vector3(randomPosition.x, randomPosition.y, 0f);

            Instantiate(
                _enemyPrefab,
                spawnPosition,
                Quaternion.identity
            );

            if (i < _spawnCount - 1)
            {
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        _isSpawning[spawnIndex] = false;
        _isSpawned[spawnIndex] = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform spawnPoint = transform.GetChild(i);

            Gizmos.DrawWireSphere(
                spawnPoint.position,
                _spawnDistance
            );
        }
    }
}
