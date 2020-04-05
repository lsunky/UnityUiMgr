using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingWindow : UiBase
{
    public Transform pointRoot;
    private List<GameObject> pointList;
    private int curIndex;

    public int CurIndex
    {
        get
        {
            return curIndex;
        }

        set
        {
            curIndex = value;
            for (int i = 0; i < pointList.Count; i++)
            {
                pointList[i].SetActive(i < curIndex);
            }
        }
    }

    protected override void OnClose()
    {
        CancelInvoke();
    }

    protected override void OnInitialize()
    {
        pointList = new List<GameObject>();
        foreach (Transform item in pointRoot)
        {
            pointList.Add(item.gameObject);
        }
    }

    protected override void OnOpen(object arg)
    {
        CurIndex = 0;

        InvokeRepeating("RefreshPoint", 0.5f, 0.5f);
    }

    protected void RefreshPoint()
    {
        CurIndex = (CurIndex + 1) % (pointList.Count+1);
    }
}
