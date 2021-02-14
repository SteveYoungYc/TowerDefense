using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private Dictionary<UIType, GameObject> UIDic;

    public UIManager()
    {
        UIDic = new Dictionary<UIType, GameObject>();
    }

    public GameObject GetSingleUI(UIType type)
    {
        GameObject parent = GameObject.Find("Canvas");
        if (!parent)
        {
            Debug.LogError("No canvas");
            return null;
        }

        if (UIDic.ContainsKey(type))
        {
            return UIDic[type];
        }

        GameObject UI = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
        UI.name = type.Name;
        UIDic.Add(type, UI);
        return UI;
    }

    public void DestroyUI(UIType type)
    {
        if (UIDic.ContainsKey(type))
        {
            GameObject.Destroy(UIDic[type]);
            UIDic.Remove(type);
        }
    }
}
