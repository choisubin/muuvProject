using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, System.IDisposable
{
    // Start is called before the first frame update
    [SerializeField]
    private BaseApplication[] _app;
    void Start()
    {
        InitHandlers();
        ChangeState(EGameState.INGAME);
        NotificationCenter.Instance.PostNotification(NotiMessage.TestNoti);
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


    #region State Handlers
    private Dictionary<EGameState, IGameBasicModule> _handlers = new Dictionary<EGameState, IGameBasicModule>();
    private EGameState _currentState = EGameState.UNKNOWN;

    private void InitHandlers()
    {
        _handlers.Clear();
        _handlers.Add(EGameState.INGAME, _app[0]);
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
            }
            IGameBasicModule enterHandler = GetStateHandler(_currentState);
            if (enterHandler != null)
            {
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
}

public enum EGameState
{
    UNKNOWN,
    INGAME,
}