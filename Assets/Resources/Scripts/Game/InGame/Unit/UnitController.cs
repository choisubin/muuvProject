using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : UnitElement
{
    public void Init()
    {
        //NotificationCenter.Instance.AddObserver(OnNotification, NotiMessage.TestNoti);
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
        //switch (noti.msg)
        //{
        //    case NotiMessage.TestNoti:
        //        SungJun(noti);
        //        break;
        //}
    }

    void SungJun(Notification noti)
    {

    }
}