using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSystem
{
    private SceneState sceneState;

    public void SetScene(SceneState state)
    {
        sceneState?.OnExit();
        sceneState = state;
        sceneState?.OnEnter();
    }
}
