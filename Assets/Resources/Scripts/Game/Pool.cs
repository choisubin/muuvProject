using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject Original { get; private set; }
    public Transform Root { get; set; }

    Stack<PoolAble> _poolStack = new Stack<PoolAble>();

    public void Init(GameObject original, int count = 5)
    {
        Original = original;
        // UnityChan_Root 빈 오브젝트 생성. 
        Root = new GameObject().transform;
        Root.name = $"{original.name}_Root";

        // count 개수의 오브젝트들을 UnityChan_Root의 자식으로. 이 5 개를 재활용할 것 👉 오브젝트 풀링 
        for (int i = 0; i < count; i++)
            Push(Create());
    }

    PoolAble Create()
    {
        GameObject go = Object.Instantiate<GameObject>(Original);
        go.name = Original.name; // 뒤에 붙는 (Clone) 없앰. 원본 프리팹과 이름 같게.
        PoolAble poolable = go.GetComponent<PoolAble>();
        if (poolable == null)
            poolable = go.AddComponent<PoolAble>();
        //return go.GetOrAddComponent<PoolAble>();
        return poolable;
    }

    public void Push(PoolAble poolable) // 풀에 넣어주기 (오브젝트 비활성화)
    {
        if (poolable == null)
            return;

        poolable.transform.parent = Root;
        poolable.gameObject.SetActive(false);
        poolable.IsUsing = false;

        _poolStack.Push(poolable);
    }

    public PoolAble Pop(Transform parent) // 풀로부터 꺼내오기 (오브젝트 활성화)
    {
        PoolAble poolable;

        if (_poolStack.Count > 0) // 스택(대기상태)이 빈 크기 X 즉 하나라도 재활용 할 수 있는 애가 있다면 
            poolable = _poolStack.Pop();
        else // 스택(대기상태)이 지금 비었다면 재활용 할 수 있는 애가 없으므로 새로 만들어야
            poolable = Create();

        poolable.gameObject.SetActive(true);  // 활성화 (poolable.gameObject로 접근해서 활성화)

        // DontDestroyOnLoad 해제 용도
        //if (parent == null)
        //    poolable.transform.parent = PoolCenter.Scene.CurrentScene.transform;

        // poolable 👉 풀에서 꺼낸 오브젝트의 Poolable
        poolable.transform.parent = parent; // 파라미터로 받은 parent 를 부모로 설정
        poolable.IsUsing = true;

        return poolable;
    }
}