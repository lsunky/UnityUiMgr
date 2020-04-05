using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : UiBase {
    public Button btnNext;
    public Button btnBag;
    protected override void OnInitialize()
    {
        btnNext.onClick.AddListener(btnNextClickHandle);
        btnBag.onClick.AddListener(btnBagClickHandle);
    }

    private void btnBagClickHandle()
    {
        UiMgr.Instance.ClearRecord();
        UiMgr.Instance.GC();
        UiMgr.Instance.Open(WindowId.Bag,null);
    }

    private void btnNextClickHandle()
    {
        UiMgr.Instance.GC();
        Close(true);
    }
}
