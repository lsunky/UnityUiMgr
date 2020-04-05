using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TextAsset asset = Resources.Load<TextAsset>("AssetswindowConfig");
        List<WindowConfig> list = new List<WindowConfig>();
        string[] striparr = asset.text.Split(new string[] { "\n" }, StringSplitOptions.None);
        striparr = striparr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
        foreach (var item in striparr)
        {
            WindowConfig windowConfig = JsonUtility.FromJson<WindowConfig>(item);
            list.Add(windowConfig);
        }

        UiMgr.Instance.Init(transform, list);
        UiMgr.Instance.Open(WindowId.Login, null);
    }
	
	
}
