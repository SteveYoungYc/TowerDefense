using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private static readonly string path = "Prefabs/UI/Panel/MainPanel";
    
    public MainPanel():base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("ExitBtn").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
            Pop();
        });
    }
}
