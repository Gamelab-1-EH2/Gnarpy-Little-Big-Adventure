using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI_System;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    private UI_AudioController audioController;

    private void Awake()
    {
        audioController = new UI_AudioController();
    }

    private void Start()
    {
        audioController.AddSliders(GetComponentsInChildren<UIAudioSlider>().ToList());
    }

    private void OnDestroy()
    {
        audioController.Disconnect();
    }
}
