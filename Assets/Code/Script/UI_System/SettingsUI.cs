using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI_System
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private List<UIAudioSlider> _sliders;
        private UI_AudioController _audioController;

        private void Awake()
        {
            _sliders.AddRange(GetComponentsInChildren<UIAudioSlider>());
            _audioController.AddSliders(_sliders);
        }

        private void OnEnable()
        {
            _audioController.UpdateVolumeValues();
        }
    }
}