using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;

// e 키로 이동

public class CBossEntrance : MonoBehaviour
{
    [Header("씬 설정")]
    [SerializeField] private string _bossSceneName = "BossRoomScene";

    private bool _isPlayerNear;
    private bool _isLoading;

    private void Update()
    {
        if (!_isPlayerNear || _isLoading)
        {
            return;
        }

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            _isLoading = true;

            SceneManager.LoadScene(_bossSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            _isPlayerNear = true;

            Debug.Log("E키를 눌러 보스방에 입장하세요.");
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
