using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Definition : MonoBehaviour
{

}

[Serializable]
public class StageDefinition
{
    public int key;
    public StageDetailDefinition[] detailStage;
}

[Serializable]
public class StageDetailDefinition
{
    public int key;
    public int stageNum;
    public int semiStageNum;
    public int stageDetailMapKey;
}

public class StageDetailMapDefinition
{
    public int key;
    public int width;
    public int height;
    public int[][] mapGrid;
}

[Serializable]
public class StageDetailDefinitionWrapper : ILoader<int, StageDetailDefinition>
{
    public List<StageDetailDefinition> definitions = new List<StageDetailDefinition>();
    public Dictionary<int, StageDetailDefinition> MakeDict()
    {
        Dictionary<int, StageDetailDefinition> dict = new Dictionary<int, StageDetailDefinition>();
        foreach (StageDetailDefinition definition in definitions)
        {
            Debug.LogError(definition.key + " " + definition);
            dict.Add(definition.key, definition);
        }
        return dict;
    }
}