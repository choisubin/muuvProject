using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : UnitElement
{
    public void Init()
    {
        NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.TestNoti);
    }
    public void Set()
    {

    }

    public void AdvanceTime(float dt_sec)
    {

    }

    public void Dispose()
    {

    }
    void OnNotification(Notification noti)
    {
        switch (noti.msg)
        {
            case ENotiMessage.TestNoti:
                SungJun(noti);
                break;
        }
    }

    void SungJun(Notification noti)
    {

    }
}