using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiMgr
{
    private Dictionary<WindowId, WindowConfig> dicWindowConfig;
    private static UiMgr instance;
    public static UiMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UiMgr();
            }
            return instance;
        }
    }

    private Transform uiRoot;
    private Dictionary<WindowId, UiBase> dicWindows;
    private Stack<WindowId> stackOpen;

    private WindowId curWindowId = WindowId.None;
    public UiMgr()
    {
        dicWindows = new Dictionary<WindowId, UiBase>();
        stackOpen = new Stack<WindowId>();
        dicWindowConfig = new Dictionary<WindowId, WindowConfig>();
    }

    private bool CheckStackRecord(WindowId windowId)
    {
        if (stackOpen.Contains(windowId))
        {
            while (stackOpen.Pop() != windowId)
            {
                //Debug.LogError("移除重复的ID:"+windowId.ToString());
            }
            return true;
        }
        return false;
    }

    private UiBase GetUiWindow(WindowId windowId)
    {
        UiBase uiBase;
        if (!dicWindows.TryGetValue(windowId, out uiBase))
        {
            string prefabPath = "Prefab/Window/" + Enum.GetName(typeof(WindowId), windowId) + "Window";
            UiBase prefab = Resources.Load<UiBase>(prefabPath);
            uiBase = GameObject.Instantiate<UiBase>(prefab, uiRoot);
            uiBase.Initialize(CloseHandle,windowId, TranslationComplete);
            dicWindows[windowId] = uiBase;
        }
        return uiBase;
    }

    private void CloseHandle(WindowId windowId)
    {
        if (curWindowId == windowId)
        {
            if (stackOpen.Count > 0)
            {
                windowId = stackOpen.Pop();
            }
            else
            {
                windowId = WindowId.Main;
            }
            UiBase uiBase = GetUiWindow(windowId);
            uiBase.Open(null, false);
            curWindowId = windowId;
        }
        else
        {
            Debug.LogError(string.Format("you are not top ,you cannot close by self,top is{0},and self is{1}",curWindowId,windowId));
        }
    }

    private void DestroyWindow(WindowId windowId)
    {
        UiBase uibase;
        if (dicWindows.TryGetValue(windowId,out uibase))
        {
            GameObject.Destroy(uibase.gameObject);
            dicWindows.Remove(windowId); 
        }
    }

    private void TranslationComplete()
    {
        ClosePop(WindowId.Translation);
    }
    /// <summary>
    /// 把不需要到回收掉
    /// </summary>
    public void GC()
    {
        List<WindowId> willRemoveList = new List<WindowId>();
        foreach (var kv in dicWindows)
        {
            if (!dicWindowConfig.ContainsKey(kv.Key) && curWindowId != kv.Key)
            {
                willRemoveList.Add(kv.Key);
            }
        }

        foreach (var windowId in willRemoveList)
        {
            DestroyWindow(windowId);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="uiRoot"></param>
    public void Init(Transform uiRoot,List<WindowConfig> listWindows)
    {
        this.uiRoot = uiRoot;
        foreach (var item in dicWindows.Keys)
        {
            DestroyWindow(item);
        }
        ClearRecord();
        dicWindowConfig.Clear();
        foreach (var item in listWindows)
        {
            dicWindowConfig[item.windowId] = item;
        }
    }

    /// <summary>
    /// 清空逻辑层历史记录
    /// </summary>
    public void ClearRecord()
    {
        stackOpen.Clear();
    }

    /// <summary>
    /// 打开一个新界面，并关闭上一个界面
    /// </summary>
    /// <param name="windowId"></param>
    /// <param name="arg"></param>
    public UiBase Open(WindowId windowId, object arg)
    {
        if (curWindowId != windowId)
        {
            CheckStackRecord(windowId);
            UiBase uiBase;
            if (curWindowId != WindowId.None)
            {
                uiBase = GetUiWindow(curWindowId);
                uiBase.Close(false);
            }

            if (dicWindowConfig[windowId].needTranslation)
            {
                Popup(WindowId.Translation, null);
            }
            uiBase = GetUiWindow(windowId);
            uiBase.Open(arg, false);
            curWindowId = windowId;
            return uiBase;
        }
        else
        {
            Debug.LogError("cur is show,but you still try to open it:" + windowId);
        }
        return null;
    }

    /// <summary>
    /// 跳转到windowId，关闭后要回到上个界面
    /// </summary>
    /// <param name="windowId"></param>
    /// <param name="arg"></param>
    public UiBase Jump(WindowId windowId, object arg)
    {
        if (curWindowId != windowId)
        {
            CheckStackRecord(windowId);
            UiBase uiBase;
            if (curWindowId != WindowId.None)
            {
                uiBase = GetUiWindow(curWindowId);
                uiBase.Close(false);
            }
            stackOpen.Push(curWindowId);

            if (dicWindowConfig[windowId].needTranslation)
            {
                Popup(WindowId.Translation, null);
            }
            uiBase = GetUiWindow(windowId);
            uiBase.Open(arg, false);
            curWindowId = windowId;
            return uiBase;
        }
        else
        {
            Debug.LogError("cur is show,but you still try to Jump it:" + windowId);
        }
        return null;
    }

    /// <summary>
    /// 开启一个pop
    /// </summary>
    /// <param name="windowId"></param>
    /// <param name="arg"></param>
    public UiBase Popup(WindowId windowId, object arg)
    {
        UiBase uiBase = GetUiWindow(windowId);
        uiBase.Open(arg, true);
        return uiBase;
    }

    /// <summary>
    /// 关闭pop
    /// </summary>
    /// <param name="windowId"></param>
    public void ClosePop(WindowId windowId)
    {
        UiBase uiBase = GetUiWindow(windowId);
        if (uiBase.isPop)
        {
            uiBase.Close(false);
        }
        else
        {
            Debug.LogError("window is not pop,you cannot closepop:" + windowId.ToString());
        }
    }

    public void OpenLoading()
    {
        Popup(WindowId.Loading,null);
    }

    public void CloseLoading()
    {
        ClosePop(WindowId.Loading);
    }
}
