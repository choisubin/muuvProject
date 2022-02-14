using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static NotificationCenter;

public class NotificationCenter : MonoBehaviour
{
    #region Singelton
    private static bool _appIsClosing = false;
    private static NotificationCenter _instance;
    public static NotificationCenter Instance
    {
        get
        {
            if (_appIsClosing)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = FindObjectOfType<NotificationCenter>();
                if (FindObjectsOfType<NotificationCenter>().Length > 1)
                {
                    Debug.LogError("[Singleton] Something went really wrong " +
                        " - there should never be more than 1 singleton!" +
                        " Reopening the scene might fix it.");
                    return _instance;
                }

                if (_instance == null)
                {
                    GameObject go = new GameObject("Default Notification Center");
                    _instance = go.AddComponent<NotificationCenter>();
                }
            }

            return _instance;
        }
    }
    void OnApplicationQuit()
    {
        // release reference on exit
        _appIsClosing = true;
    }
    #endregion

    Hashtable _notifications = new Hashtable();

    public delegate void DelFunction(Notification noti);

    #region Add Observer
 
    public void AddObserver(DelFunction observer, NotiMessage msg/*, Component sender*/)
    {
        if (_notifications[msg] == null)
        {
            _notifications[msg] = new List<DelFunction>();
        }

        List<DelFunction> notifyList = _notifications[msg] as List<DelFunction>;

        if (!notifyList.Contains(observer)) { notifyList.Add(observer); }
    }
    #endregion

    #region Remove Observer
    public void RemoveObserver(DelFunction observer, NotiMessage msg)
    {
        List<DelFunction> notifyList = (List<DelFunction>)_notifications[msg];

        if (notifyList != null)
        {
            if (notifyList.Contains(observer))
            {
                notifyList.Remove(observer);
            }

            if (notifyList.Count == 0)
            {
                _notifications.Remove(msg);
            }
        }
    }
    #endregion

    #region Post Notification
    public void PostNotification(NotiMessage aMsg)
    {
        PostNotification(aMsg, null);
    }

    public void PostNotification(NotiMessage aMsg, Hashtable aData)
    {
        PostNotification(new Notification(aMsg, aData));
    }

    public void PostNotification(Notification aNotification)
    {
        List<DelFunction> notifyList = (List<DelFunction>)_notifications[aNotification.msg];
        if (notifyList == null)
        {
            Debug.Log("Notify list not found in PostNotification: " + aNotification.msg);
            return;
        }

        List<DelFunction> observersToRemove = new List<DelFunction>();
      
        foreach (DelFunction observer in notifyList)
        {
            if (observer == null)
            {
                observersToRemove.Add(observer);
            }
            else
            {
                observer(aNotification);
            }
        }

        foreach (DelFunction observer in observersToRemove)
        {
            notifyList.Remove(observer);
        }
    }
    #endregion
    
}
public class Notification
{
    public NotiMessage msg;
    public Hashtable data;
    private static Notification _instance = new Notification(NotiMessage.UNKNOWN , null);
    public Notification(NotiMessage aMsg)
    {
        msg = aMsg;
        data = null;
    }

    public Notification(NotiMessage aMsg, Hashtable aData)
    {
        msg = aMsg;
        data = aData;
    }

    public static Notification Instantiate(NotiMessage aMsg, Hashtable aData)
    {
        _instance.msg = aMsg;
        if(aData == null)
        {
            _instance.data = new Hashtable();
        }
        else
        {
            _instance.data = aData;
        }
        return _instance;
    }

    public static Notification Instantiate(NotiMessage aMsg)
    {
        _instance.msg = aMsg;
        return _instance;
    }
}

public enum NotiMessage
{
    UNKNOWN = 0,
    //1000 ~ 2000 Bounce 관련
    OnBallHitGround = 1000,
    TestNoti,
}

public enum DataParamKey
{ 
    Integer, //일반 int 변수형 낱개로 보낼때
}
