using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : InGameElement
{
    public void Init()
    {
    }

    public void Set()
    {
    }
    public void AdvanceTime(float dt_sec)
    {
        app.model.time += dt_sec;
    }

    public void Dispose()
    {
    }

}
