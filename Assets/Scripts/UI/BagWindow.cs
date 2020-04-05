using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagWindow : UiBase {
    public Button btnChappier;
    public Button btnClose;
    protected override void OnInitialize()
    {
        btnChappier.onClick.AddListener(btnChapterClickHandlet);
        btnClose.onClick.AddListener(btnCloseClickHandle);
    }

    private void btnCloseClickHandle()
    {
        Close(true);
    }

    private void btnChapterClickHandlet()
    {
        UiMgr.Instance.Jump(WindowId.Chapter,null);
    }

    protected override void OnOpen(object arg)
    {
        
    }

    protected override void OnClose()
    {
        
    }
}
