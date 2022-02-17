using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseElement : MonoBehaviour
{
    //public BaseApplication app
    //{
    //    get
    //    {
    //        return GameObject.FindObjectOfType<BaseApplication>();
    //    }
    //}
}

public abstract class BaseApplication : MonoBehaviour, IGameBasicModule
{
    public void SetActive(bool flag)
    {
        this.gameObject.SetActive(flag);
    }
    public abstract void Init();
    public abstract void AdvanceTime(float dt_sec);
    public abstract void Set();
    public abstract void Dispose();
}