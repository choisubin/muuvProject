using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameModel : InGameElement
{
    //현재 진행된 시간
    //ㄹㅇㄴㅁㄹㅇㄻㅇㄴㄹ
    [SerializeField]
    private float _time =0;
    public float time
    {
        get
        {
            return _time;
        }
        set
        {
            _time = value;
        }
    }
}
