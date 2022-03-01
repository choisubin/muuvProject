using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, System.IDisposable
{
    // Start is called before the first frame update
    [SerializeField]
    private Dictionary<EGameState, BaseApplication> _app = new Dictionary<EGameState, BaseApplication>();
    void Start()
    {
        InitStateApplication();
        InitHandlers();
        Debug.LogError(DefinitionManager.Instance);
        Debug.LogError(DefinitionManager.Instance.GetData<StageDetailMapDefinition>(1).height);
        Debug.LogError(DefinitionManager.Instance.GetDatas<StageDetailMapDefinition>());
        NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.ChangeSceneState);
        ChangeState(EGameState.LOBBY);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState != EGameState.UNKNOWN)
        {
            GetStateHandler(_currentState).AdvanceTime(Time.deltaTime);
        }
    }


    public void Dispose()
    {
        GetStateHandler(_currentState).Dispose();
    }

    public void OnNotification(Notification noti)
    {
        EGameState state = (EGameState)noti.data[EDataParamKey.Integer];
        ChangeState(state);
    }

    #region State Handlers
    private Dictionary<EGameState, IGameBasicModule> _handlers = new Dictionary<EGameState, IGameBasicModule>();
    private EGameState _currentState = EGameState.UNKNOWN;

    private void InitStateApplication()
    {
        GameObject go = PoolManager.Instance.GrabPrefabs(EPrefabsType.GameStateHandler, "InGameApplication", this.transform);
        _app.Add(EGameState.INGAME, go.GetComponent<InGameApplication>());
        go.SetActive(false);

        go = PoolManager.Instance.GrabPrefabs(EPrefabsType.GameStateHandler, "LobbyApplication", this.transform);
        _app.Add(EGameState.LOBBY, go.GetComponent<LobbyApplication>());
        go.SetActive(false);
    }

    private void InitHandlers()
    {
        _handlers.Clear();
        foreach (KeyValuePair<EGameState, BaseApplication> item in _app)
        {
            _handlers.Add(item.Key, item.Value);
        }

        foreach (EGameState state in _handlers.Keys)
        {
            _handlers[state].Init();
        }
    }


    private void ChangeState(EGameState nextState)
    {
        if (nextState != EGameState.UNKNOWN && nextState != _currentState)
        {
            EGameState prevState = _currentState;
            _currentState = nextState;
            IGameBasicModule leaveHandler = GetStateHandler(prevState);
            if (leaveHandler != null)
            {
                leaveHandler.Dispose();
                leaveHandler.SetActive(false);
            }
            IGameBasicModule enterHandler = GetStateHandler(_currentState);
            if (enterHandler != null)
            {
                enterHandler.SetActive(true);
                enterHandler.Set();
            }
        }
    }

    private IGameBasicModule GetStateHandler(EGameState state)
    {
        if (_handlers.ContainsKey(state))
        {
            return _handlers[state];
        }
        return null;
    }
    #endregion

}
public interface IGameBasicModule
{
    void Init();
    void Set();
    void AdvanceTime(float dt_sec);
    void Dispose();
    void SetActive(bool flag);
}

public enum EGameState
{
    UNKNOWN,
    INGAME,
    LOBBY,
}