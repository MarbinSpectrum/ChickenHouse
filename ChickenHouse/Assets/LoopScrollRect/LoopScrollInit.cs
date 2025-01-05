using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopScrollInit : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource
{
    public GameObject item;
    public LoopScrollRect loopScrollRect;

    private Stack<Transform> pool = new Stack<Transform>();
    protected List<GameObject> objList = new List<GameObject>();


    public virtual GameObject GetObject(int index)
    {
        if (pool.Count == 0)
        {
            GameObject newItem = Instantiate(item);
            objList.Add(newItem);
            return newItem;
        }
        Transform candidate = pool.Pop();
        candidate.gameObject.SetActive(true);
        return candidate.gameObject;
    }

    public virtual void ReturnObject(Transform trans)
    {
        trans.gameObject.SetActive(false);
        trans.SetParent(transform, false);
        pool.Push(trans);
    }

    public virtual void ProvideData(Transform transform, int idx)
    {
    }



    public virtual void Init(int cnt)
    {
        loopScrollRect.prefabSource = this;
        loopScrollRect.dataSource = this;
        loopScrollRect.totalCount = cnt;
        loopScrollRect.RefillCells();
    }
}
