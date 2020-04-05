using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UiBase : MonoBehaviour {
    private Action<WindowId> closeHandle;
    private WindowId id;
    public bool isPop;
    /// <summary>
    /// 对于需要切屏动画的window来说，执行此回掉关闭切屏动画
    /// </summary>
    protected Action translationComplete;
	public void Initialize(Action<WindowId> closeHandle,WindowId id,Action translationComplete)
    {
        this.id = id;
        this.closeHandle = closeHandle;
        this.translationComplete = translationComplete;
        OnInitialize();
    }
    

    public void Open(object arg,bool isPop)
    {
        this.isPop = isPop;
        gameObject.SetActive(true);
        OnOpen(arg);
    }

    public void Close(bool bySelf)
    {
        OnClose();
        gameObject.SetActive(false);
        if (bySelf && !isPop)
        {
            closeHandle(id);
        }
    }

    protected virtual void OnInitialize() { }
    protected virtual void OnOpen(object arg) { }
    protected virtual void OnClose() { }
}
