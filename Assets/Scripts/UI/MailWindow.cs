using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MailWindow : UiBase {
	public Button btnBag;
	public Button btnChat;
    public Button btnClose;
    protected override void OnInitialize()
    {
        btnBag.onClick.AddListener(btnBagClickHandle);
        btnChat.onClick.AddListener(btnChatClickHandle);
        btnClose.onClick.AddListener(btnCloseClickHandle);
    }

    private void btnCloseClickHandle()
    {
        Close(true);
    }

    private void btnBagClickHandle()
    {
        UiMgr.Instance.Open(WindowId.Bag, null);
    }

    private void btnChatClickHandle()
    {
        UiMgr.Instance.Popup(WindowId.Chat, null);
    }

}
