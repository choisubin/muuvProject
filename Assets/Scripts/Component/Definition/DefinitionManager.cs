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
    private Dictionary<int, StageDetailDefinition> _stageDetailDefinition  = new Dictionary<int, StageDetailDefinition>();
    private Dictionary<int, StageDetailMapDefinition> _stageDetailMapDefinition = new Dictionary<int, StageDetailMapDefinition>();
    
    Hashtable _definitions = new Hashtable();
    private void LoadJson<Wrapper, Definition>(string path)
    {
        path = "Definition/" + path;
        _definitions[typeof(Definition)] = LoadJson<StageDetailMapDefinitionWrapper, int, StageDetailMapDefinition>(path).MakeDict();
        Debug.LogError(_definitions[typeof(Definition)]);
    }
    private void LoadAllJson()
    {
        LoadJson<StageDetailMapDefinitionWrapper, StageDetailMapDefinition>("StageDetailMapDefinition");
    }
    //public void LoadJson()
    //{
    //    //stageDetailDefinition = LoadJson<StageDetailDefinitionWrapper, int, StageDetailDefinition>("StageDetailDefinition").MakeDict();
    //    _stageDetailMapDefinition = LoadJson<StageDetailMapDefinitionWrapper, int, StageDetailMapDefinition>("Definition/StageDetailMapDefinition").MakeDict();
    //    //DefinitionManager.Instance.GetDatas<DialogueActionDefinition>()
    //}
    Loader LoadJson<Loader>(string path)
    { 
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        Debug.LogError(textAsset.text);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public T GetDatas<T>()
    {
        if(_definitions.ContainsKey(typeof(T)))
        {
            return (T)_definitions[typeof(T)];
        }
        return default(T);
    }
}
