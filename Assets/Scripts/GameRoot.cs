using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance { get; private set; }
    public SceneSystem SceneSystem { get; private set; }
    
    public UnityAction<BasePanel> Push { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SceneSystem = new SceneSystem();
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneSystem.SetScene(new StartScene());
    }

    public void SetAction(UnityAction<BasePanel> push)
    {
        Push = push;
    }
}
