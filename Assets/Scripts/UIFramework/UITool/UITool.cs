using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理工具
/// </summary>
public class UITool
{
    /// <summary>
    /// 当前活动面板
    /// </summary>
    private GameObject activePanel;

    public UITool(GameObject panel)
    {
        activePanel = panel;
    }

    public T GetOrAddComponent<T>() where T : Component
    {
        if (activePanel.GetComponent<T>() == null)
        {
            activePanel.AddComponent<T>();
        }

        return activePanel.GetComponent<T>();
    }

    /// <summary>
    /// 根据名称查找子对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject FindChildGameObject(string name)
    {
        Transform[] transforms = activePanel.GetComponentsInChildren<Transform>();

        foreach (var transform in transforms)
        {
            if (transform.name == name)
            {
                return transform.gameObject;
            }
        }

        Debug.LogError($"{activePanel.name}里找不到{name}");
        return null;
    }
    
    public T GetOrAddComponentInChildren<T>(string name) where T : Component
    {
        GameObject child = FindChildGameObject(name);
        if (child)
        {
            if (child.GetComponent<T>() == null)
            {
                child.AddComponent<T>();
            }

            return child.GetComponent<T>();
        }

        return null;
    }
}
