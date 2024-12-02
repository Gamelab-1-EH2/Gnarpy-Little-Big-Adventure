using System;
using System.Linq;
using UnityEngine;

using UI_System;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    private UI_AudioController audioController;

    [SerializeField] private Button _mainMenuRequest;

    private void Awake()
    {
        audioController = new UI_AudioController();
        _mainMenuRequest.onClick.AddListener(CallMainMenuRequest);
    }

    private void Start()
    {
        audioController.AddSliders(GetComponentsInChildren<UIAudioSlider>().ToList());
    }

    private void CallMainMenuRequest() => GameplayUI.OnMainMenuRequest?.Invoke();
}
