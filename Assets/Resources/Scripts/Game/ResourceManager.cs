using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/'); // '/' 뒤의 이름 추출. 
            if (index >= 0)
                name = name.Substring(index + 1); // 이게 바로 프리팹의 이름.

            GameObject go = PoolCenter.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        // 풀에서 못 찾았다면 힘들게 로딩
        return Resources.Load<T>(path); // UnityEngine의 Resource.
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        if (original.GetComponent<PoolAble>() != null)
            return PoolCenter.Pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        PoolAble poolable = go.GetComponent<PoolAble>();
        if (poolable != null)
        {
            PoolCenter.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}
