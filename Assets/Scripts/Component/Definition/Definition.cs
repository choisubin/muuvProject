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

[Serializable]
public class StageDetailMapDefinition
{
    public int key;
    public int width;
    public int height;
    public int[][] mapGrid;
}

[Serializable]
public class StageDetailMapDefinitionContainer : ILoader<int, StageDetailMapDefinition>
{
    public List<StageDetailMapDefinition> definitions = new List<StageDetailMapDefinition>();
    public Dictionary<int, StageDetailMapDefinition> MakeDict()
    {
        Dictionary<int, StageDetailMapDefinition> dict = new Dictionary<int, StageDetailMapDefinition>();
        foreach (StageDetailMapDefinition definition in definitions)
        {
            dict.Add(definition.key, definition);
        }
        return dict;
    }
}
