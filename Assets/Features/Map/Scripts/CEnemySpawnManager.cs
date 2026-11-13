using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CEnemySpawnManager : MonoBehaviour
{
    [Header("적 설정")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _maxEnemyCount = 8;

    [Header("플레이어 설정")]
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _mainCamera;

    [Header("스폰 시간")]
    [SerializeField] private float _spawnIntervalMin = 3f;
    [SerializeField] private float _spawnIntervalMax = 5f;

    [Header("스폰 위치")]
    [SerializeField] private float _spawnOutsideDistance = 1.5f;
    [SerializeField] private float _minPlayerDistance = 4f;

    [Header("NavMesh 설정")]
    [SerializeField] private float _navMeshSearchRadius = 2f;
    [SerializeField] private int _spawnTryCount = 15;

    private GameObject[] _spawnedEnemies;

    private float _spawnTimer;
    private float _nextSpawnTime;
    private float _playerFindTimer;

    private Vector3 _spawnPosition;


    private void Start()
    {
        if (_maxEnemyCount < 1)
        {
            _maxEnemyCount = 1;
        }

        _spawnedEnemies = new GameObject[_maxEnemyCount];

        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }

        FindPlayer();

        SetNextSpawnTime();
    }


    private void Update()
    {
        if (_enemyPrefab == null)
        {
            return;
        }

        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;

            if (_mainCamera == null)
            {
                return;
            }
        }

        int enemyCount = CountEnemy();

        if (enemyCount >= _maxEnemyCount)
        {
            return;
        }

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _nextSpawnTime)
        {
            _spawnTimer = 0f;

            SetNextSpawnTime();

            if (FindSpawnPosition())
            {
                CreateEnemy();
            }
        }
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            _player = playerObject.transform;
        }
    }


    private void SetNextSpawnTime()
    {
        _nextSpawnTime = Random.Range(
            _spawnIntervalMin,
            _spawnIntervalMax
        );
    }


    private int CountEnemy()
    {
        int enemyCount = 0;

        for (int i = 0; i < _spawnedEnemies.Length; i++)
        {
            if (_spawnedEnemies[i] == null ||
                !_spawnedEnemies[i].activeInHierarchy)
            {
                _spawnedEnemies[i] = null;
            }
            else
            {
                enemyCount++;
            }
        }

        return enemyCount;
    }


    private bool FindSpawnPosition()
    {
        float halfHeight = _mainCamera.orthographicSize;
        float halfWidth = halfHeight * _mainCamera.aspect;

        float cameraX = _mainCamera.transform.position.x;
        float cameraY = _mainCamera.transform.position.y;

        for (int i = 0; i < _spawnTryCount; i++)
        {
            float randomX = Random.Range(-halfWidth, halfWidth);
            float randomY = Random.Range(-halfHeight, halfHeight);

            int side = Random.Range(0, 4);

            Vector3 candidate = Vector3.zero;

            if (side == 0)
            {
                candidate.x = cameraX - halfWidth - _spawnOutsideDistance;
                candidate.y = cameraY + randomY;
            }

            else if (side == 1)
            {
                candidate.x = cameraX + halfWidth + _spawnOutsideDistance;
                candidate.y = cameraY + randomY;
            }

            else if (side == 2)
            {
                candidate.x = cameraX + randomX;
                candidate.y = cameraY - halfHeight - _spawnOutsideDistance;
            }

            else
            {
                candidate.x = cameraX + randomX;
                candidate.y = cameraY + halfHeight + _spawnOutsideDistance;
            }

            candidate.z = _player.position.z;

            NavMeshHit hit;

            if (!NavMesh.SamplePosition(
                candidate,
                out hit,
                _navMeshSearchRadius,
                NavMesh.AllAreas))
            {
                continue;
            }

            Vector3 checkPosition = hit.position;

            if (checkPosition.x >= cameraX - halfWidth &&
                checkPosition.x <= cameraX + halfWidth &&
                checkPosition.y >= cameraY - halfHeight &&
                checkPosition.y <= cameraY + halfHeight)
            {
                continue;
            }

            float distance = Vector2.Distance(
                _player.position,
                checkPosition
            );

            if (distance < _minPlayerDistance)
            {
                continue;
            }
            _spawnPosition = checkPosition;
            _spawnPosition.z = _player.position.z;

            return true;
        }

        return false;
    }

    private void CreateEnemy()
    {
        for (int i = 0; i < _spawnedEnemies.Length; i++)
        {
            if (_spawnedEnemies[i] == null)
            {
                GameObject enemy = Instantiate(
                    _enemyPrefab,
                    _spawnPosition,
                    Quaternion.identity
                );

                _spawnedEnemies[i] = enemy;

                return;
            }
        }
    }
}
