using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginWindow : UiBase {

	public Button btnSure;

    protected override void OnClose()
    {

    }

    protected override void OnInitialize()
    {
        btnSure.onClick.AddListener(btnSureClickHandle);
    }

    protected override void OnOpen(object arg)
    {

    }

    private void btnSureClickHandle()
    {
        UiMgr.Instance.Open(WindowId.Main, null);
    }
}
