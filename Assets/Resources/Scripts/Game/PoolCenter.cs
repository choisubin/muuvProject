using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCenter : MonoBehaviour
{
    #region Singelton
    private static bool _appIsClosing = false;
    private static PoolCenter _instance;
    public static PoolCenter Instance
    {
        get
        {
            if (_appIsClosing)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolCenter>();
                if (FindObjectsOfType<PoolCenter>().Length > 1)
                {
                    Debug.LogError("[Singleton] Something went really wrong " +
                        " - there should never be more than 1 singleton!" +
                        " Reopening the scene might fix it.");
                    return _instance;
                }

                if (_instance == null)
                {
                    GameObject go = new GameObject("Default Pool Center");
                    _instance = go.AddComponent<PoolCenter>();
                }
            }

            return _instance;
        }
    }
    void OnApplicationQuit()
    {
        // release reference on exit
        _appIsClosing = true;
    }
    #endregion

    PoolManager _pool = new PoolManager();

    public static PoolManager Pool { get { return Instance._pool; } }

    static void Init()
    {
        _instance._pool.Init();

    }

    public static void Clear()
    {
        Pool.Clear();
    }
}
