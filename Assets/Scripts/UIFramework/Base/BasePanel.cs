﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel
{
    public UIType UIType { get; private set; }
    public UITool UITool { get; private set; }
    public PanelManager PanelManager { get; private set; }
    public UIManager UIManager { get; private set; }

    public BasePanel(UIType type)
    {
        UIType = type;
    }

    public void Initialize(UITool tool)
    {
        UITool = tool;
    }
    
    public void Initialize(PanelManager panelManager)
    {
        PanelManager = panelManager;
    }
    
    public void Initialize(UIManager manager)
    {
        UIManager = manager;
    }
    
    //UI进入时执行，只执行一次
    public virtual void OnEnter() {}

    public virtual void OnPause()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public virtual void OnResume()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public virtual void OnExit()
    {
        UIManager.DestroyUI(UIType);
    }

    public void Push(BasePanel panel) => PanelManager.Push(panel);

    public void Pop() => PanelManager.Pop();
}
