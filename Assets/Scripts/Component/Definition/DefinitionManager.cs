﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DefinitionManager : MonoBehaviour 
{
    #region Singelton
    private static DefinitionManager _instance;
    public static DefinitionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DefinitionManager>();
                if (FindObjectsOfType<DefinitionManager>().Length > 1)
                {
                    Debug.LogError("[Singleton] Something went really wrong " +
                        " - there should never be more than 1 singleton!" +
                        " Reopening the scene might fix it.");
                    return _instance;
                }

                if (_instance == null)
                {
                    GameObject go = new GameObject("DefinitionManager");
                    _instance = go.AddComponent<DefinitionManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    public void Awake()
    {
        LoadAllJson();
    }

    Hashtable _definitions = new Hashtable();
    private void LoadAllJson()
    {
        _definitions[typeof(StageDetailMapDefinition)] = LoadJson<StageDetailMapDefinitionContainer, int, StageDetailMapDefinition>("Definition/StageDetailMapDefinition").MakeDict();
    
    }
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public Dictionary<int, T> GetDatas<T>()
    {
        if(_definitions.ContainsKey(typeof(T)))
        {
            return _definitions[typeof(T)] as Dictionary<int, T>;
        }
        return null;
    }
    public T GetData<T>(int key)
    {
        if (_definitions.ContainsKey(typeof(T)))
        {
            Dictionary<int, T> definition = _definitions[typeof(T)] as Dictionary<int, T>;
            return definition[key];
        }
        return default(T);
    }
}
