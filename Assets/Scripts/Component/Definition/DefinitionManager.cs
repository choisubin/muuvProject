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
        LoadJson();
    }
    public Dictionary<int, StageDetailDefinition> stageDetailDefinition { get; private set; } = new Dictionary<int, StageDetailDefinition>();
    public StageDetailMapDefinition StageDetailMapDefinition { get; private set; } = new StageDetailMapDefinition();
    public void LoadJson()
    {
        //stageDetailDefinition = LoadJson<StageDetailDefinitionWrapper, int, StageDetailDefinition>("StageDetailDefinition").MakeDict();
        StageDetailMapDefinition = LoadJson<StageDetailMapDefinition>("Definition/StageDetailMapDefinition");
        Debug.LogError(DefinitionManager.Instance.StageDetailMapDefinition.key);
        Debug.LogError(DefinitionManager.Instance.StageDetailMapDefinition.width);
        Debug.LogError(DefinitionManager.Instance.StageDetailMapDefinition.height);
        foreach(var a in StageDetailMapDefinition.mapGrid)
        {
            Debug.Log(a);
        }

    }
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
}
