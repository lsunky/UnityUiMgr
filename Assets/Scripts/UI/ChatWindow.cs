using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatWindow : UiBase {
    public Button btnClose;

    protected override void OnClose()
    {
        
    }

    protected override void OnInitialize()
    {
        btnClose.onClick.AddListener(btnCloseClickHandle);
    }

    protected override void OnOpen(object arg)
    {

    }

    private void btnCloseClickHandle()
    {
        Close(true);
    }
}
