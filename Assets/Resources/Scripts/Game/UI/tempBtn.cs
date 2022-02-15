using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempBtn : MonoBehaviour
{
    public void OnClick()
    {
        Hashtable sendData = new Hashtable();
        sendData.Add(EDataParamKey.Integer, EGameState.INGAME);
        NotificationCenter.Instance.PostNotification(Notification.Instantiate(ENotiMessage.ChangeSceneState, sendData));
    }
}
