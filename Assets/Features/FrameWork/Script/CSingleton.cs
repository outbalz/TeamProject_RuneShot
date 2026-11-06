using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CSingleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject(typeof(T).ToString());
                _instance = go.AddComponent<T>();
                Debug.Log($"[CSingleton] {typeof(T).ToString()} 인스턴스가 존재하지 않아 새로 생성되었습니다.");
            }

            return _instance;
        }
    }

    public static T Create()
    {
        return CSingleton<T>.Instance;
    }

    protected virtual void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this as T;
        }

        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }
}
