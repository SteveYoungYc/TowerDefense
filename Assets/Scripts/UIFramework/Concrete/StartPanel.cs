using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private static readonly string path = "Prefabs/UI/Panel/StartPanel";
    
    public StartPanel():base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("SettingBtn").onClick.AddListener(() =>
        {
            Push(new SettingPanel());
        });
        
        UITool.GetOrAddComponentInChildren<Button>("StartBtn").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new MainScene());
        });
    }
}
