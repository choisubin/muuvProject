using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MatchTileMapElement : BaseElement
{
    public MatchTileMapApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<MatchTileMapApplication>();
        }
    }
}

public class MatchTileMapApplication : BaseApplication
{
    public MatchTileMapModel model;
    public MatchTileMapView view;
    public MatchTileMapController controller;

    //여기서 unitapplication 가지고 있을듯
    //init or Set하며 받아오는 Stage map의 정보에 따라 controller에서
    //프리팹 생성같은거 해서 전체적으로는 MatchTileMappApp에서 가지고 있고
    //컨트롤러에서 동적으로 해주는 방식?
    //처음 기본 데이터 받아올때만 요렇게.
    //아마 플레이어가 설정한 덱 데이터도 여까지 매개변수로 가지고 올듯?!
    private UnitApplication[][] _unitApplications; 

    public override void Init()
    {
        //stage 종류 받아옴
        controller.Init();
    }

    public override void Set()
    {
        //여기서 map정보를 GetData해와서 model에 넣어줌
        //아마 Set 아니면 Init에서 할듯
        //model.Set(GetData(map.stage); 대충 요런느낌?

        controller.Set();
    }
    public override void AdvanceTime(float dt_sec)
    {
        controller.AdvanceTime(dt_sec);
    }

    public override void Dispose()
    {
        controller.Dispose();
    }

}
