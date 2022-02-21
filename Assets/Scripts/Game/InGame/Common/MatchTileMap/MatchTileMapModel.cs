using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTileMapModel : MatchTileMapElement
{
    //현재 맵 정보 ( 어떻게 생긴 맵인지 json) int arr로
    private int[][] _mapInfo; //임시 이름임
    public int[][] MapInfo  //임시 이름임
    {
        get
        {
            return _mapInfo;
        }
    }

    public void Set(int [][] mapData)
    {
        _mapInfo = mapData;
    }
}
