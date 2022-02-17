using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void Push(PoolAble poolable)
    {
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count); // Init 을 통해 해당 Pool은 DontDestroyOnLoad가 된다.
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public PoolAble Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false) // Key는 원본 프리팹 이름으로 저장되므로 해당 프리팹으로 만든 오브젝트풀이 있나 검색. 
            CreatePool(original); // 없다면 새로운 풀을 만든다. 

        return _pool[original.name].Pop(parent); // 풀이 없다면 여기서 런타임 에러 날 것이므로 위의 과정을 해주는 것. 아룸아 original.name인 풀이 아직 없다면 만들어주기.
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}