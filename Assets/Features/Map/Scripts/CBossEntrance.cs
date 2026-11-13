using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;

public class CBossEntrance : MonoBehaviour
{
    [Header("씬 설정")]
    [SerializeField] private string _bossSceneName = "BossRoomScene";

    private bool _isPlayerNear;
    private bool _isLoading;

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (!_isPlayerNear || _isLoading)
        {
            return;
        }

        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            EnterBossRoom();
        }
    }

    private void EnterBossRoom()
    {
        if (string.IsNullOrEmpty(_bossSceneName))
        {
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(_bossSceneName))
        {
            return;
        }

        _isLoading = true;
        SceneManager.LoadScene(_bossSceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            _isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            _isPlayerNear = false;
        }
    }
}
