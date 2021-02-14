using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager
{
    private Stack<BasePanel> panelStack;
    private UIManager UIManager;
    private BasePanel panel;

    public PanelManager()
    {
        panelStack = new Stack<BasePanel>();
        UIManager = new UIManager();
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <param name="nextPanel"></param>
    public void Push(BasePanel nextPanel)
    {
        if (panelStack.Count > 0)
        {
            panel = panelStack.Peek();
            panel.OnPause();
        }
        panelStack.Push(nextPanel);
        GameObject panelGo = UIManager.GetSingleUI(nextPanel.UIType);
        nextPanel.Initialize(new UITool(panelGo));
        nextPanel.Initialize(this);
        nextPanel.Initialize(UIManager);
        nextPanel.OnEnter();
    }

    /// <summary>
    /// 面板出栈
    /// </summary>
    public void Pop()
    {
        if (panelStack.Count > 0)
        {
            panelStack.Pop().OnExit();
        }

        if (panelStack.Count > 0)
        {
            panelStack.Peek().OnResume();
        }
    }

    public void PopAll()
    {
        while (panelStack.Count > 0)
        {
            panelStack.Pop().OnExit();
        }
    }
}
