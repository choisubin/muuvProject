using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseElement: MonoBehaviour
{
}
public class BaseApplication : MonoBehaviour, IGameBasicModule
{
    public virtual void Init()
    {
    }
    public virtual void AdvanceTime(float dt_sec)
    {
    }

    public virtual void Set()
    {
    }

    public virtual void Dispose()
    {
    }
}
