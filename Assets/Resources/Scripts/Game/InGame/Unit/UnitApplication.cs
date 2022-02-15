using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitElement : BaseElement
{
    public UnitApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<UnitApplication>();
        }
    }
}

public class UnitApplication : BaseApplication
{
    public UnitModel model;
    public UnitView view;
    public UnitController controller;

    public override void Init()
    {
        controller.Init();
    }
    public override void AdvanceTime(float dt_sec)
    {
        controller.AdvanceTime(dt_sec);
    }

    public override void Set()
    {
        controller.Set();
    }

    public override void Dispose()
    {
        controller.Dispose();
    }
}
