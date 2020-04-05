using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : UiBase {

	public Button btnMail;
	public Button btnBag;
	public Button btnChat;
	public Button btnChapter;

    private GameObject goMainCity;
    protected override void OnInitialize()
    {
		btnMail.onClick.AddListener(btnMailClickHandle);
        btnBag.onClick.AddListener(btnBagClickHandle);
        btnChat.onClick.AddListener(btnChatClickHandle);
        btnChapter.onClick.AddListener(btnChapterClickHandle);
        goMainCity = GameObject.Find("MainCity");
        if (goMainCity == null)
        {
            goMainCity = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/MainCity/MainCity"));
            goMainCity.name = "MainCity";
            goMainCity.SetActive(false);
        }
    }

    protected override void OnOpen(object arg)
    {
        goMainCity.SetActive(true);
    }

    protected override void OnClose()
    {
        goMainCity.SetActive(false);
    }

    private void btnMailClickHandle()
    {
        UiMgr.Instance.Open(WindowId.Mail,null);
    }

    private void btnBagClickHandle()
    {
        UiMgr.Instance.Open(WindowId.Bag, null);
    }

    private void btnChatClickHandle()
    {
        UiMgr.Instance.Popup(WindowId.Chat, null);
    }

    private void btnChapterClickHandle()
    {
        UiMgr.Instance.Jump(WindowId.Chapter, null);
    }
}
