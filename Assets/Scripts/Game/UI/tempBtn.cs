using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempBtn : MonoBehaviour
{
    List<GameObject> list = new List<GameObject>();
    public void OnClick()
    {
        Hashtable sendData = new Hashtable();
        sendData.Add(EDataParamKey.Integer, EGameState.INGAME);
        NotificationCenter.Instance.PostNotification(Notification.Instantiate(ENotiMessage.ChangeSceneState, sendData));
    }

    public void OnClick2()
    {
        Hashtable sendData = new Hashtable();
        sendData.Add(EDataParamKey.Integer, EGameState.LOBBY);
        NotificationCenter.Instance.PostNotification(Notification.Instantiate(ENotiMessage.ChangeSceneState, sendData));
    }
    public void OnClick3()
    {
        GameObject go = PoolManager.Instance.GrabPrefabs(EPrefabsType.GameStateHandler, "test", this.transform);
        list.Add(go);
    }

    public void OnClick4()
    { 
        PoolManager.Instance.DespawnObject(EPrefabsType.GameStateHandler, list[0]);
        list.Remove(list[0]);
    }
}
