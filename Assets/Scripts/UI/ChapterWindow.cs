using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChapterWindow : UiBase {
    public Button btnStart;
    public Button btnClose;

    protected override void OnClose()
    {
        CancelInvoke();
    }

    protected override void OnInitialize()
    {
        btnStart.onClick.AddListener(btnStartClickHandle);
        btnClose.onClick.AddListener(btnCloseClickHandle);
    }

    protected override void OnOpen(object arg)
    {
        Invoke("loadResComplete", 3);//假设这里需要加载一些资源，或者网络请求量大，此界面要显示切屏动画，防止界面出现假图。
    }

    private void btnCloseClickHandle()
    {
        Close(true);
    }

    private void btnStartClickHandle()
    {
        UiMgr.Instance.OpenLoading();
        UiMgr.Instance.GC();
        Invoke("closeLoading", UnityEngine.Random.Range(4,15));
    }

    void closeLoading()
    {
        UiMgr.Instance.CloseLoading();
        UiMgr.Instance.Jump(WindowId.Battle,null);
    }

    /// <summary>
    /// 数据准备完成，界面刷新完成后，关闭切屏动画
    /// </summary>
    void loadResComplete()
    {
        translationComplete();
    }
}
