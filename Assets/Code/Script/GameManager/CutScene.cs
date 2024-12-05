using System;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public static Action OnCutSceneEnded;
    public void EndCutScene() => OnCutSceneEnded?.Invoke();
}
