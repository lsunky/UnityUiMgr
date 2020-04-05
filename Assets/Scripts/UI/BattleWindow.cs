using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleWindow : UiBase {

    private int tempNum;
    public Text txtCountDown;
    protected override void OnOpen(object arg)
    {
        tempNum = 10;
        txtCountDown.text = tempNum.ToString();
        InvokeRepeating("CountDown",1,1);
    }

    void CountDown()
    {
        tempNum--;
        txtCountDown.text = tempNum.ToString();
        if (tempNum == 0)
        {
            UiMgr.Instance.Open(WindowId.Result,null);
        }
    }

    protected override void OnInitialize()
    {
        
    }

    protected override void OnClose()
    {
        CancelInvoke();
    }
}
