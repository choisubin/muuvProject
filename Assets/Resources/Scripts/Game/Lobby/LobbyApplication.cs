using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LobbyElement : BaseElement
{
    public LobbyApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<LobbyApplication>();
        }
    }
}
public class LobbyApplication : BaseApplication
{
    public InGameModel model;
    public LobbyView view;
    public LobbyController controller;
    public override void Init()
    {
        controller.Init();
    }

    public override void Set()
    {
        //gameObject.SetActive(true);
        controller.Set();
    }
    public override void AdvanceTime(float dt_sec)
    {
        controller.AdvanceTime(dt_sec);
    }

    public override void Dispose()
    {
        //gameObject.SetActive(false);
        controller.Dispose();
    }
}
