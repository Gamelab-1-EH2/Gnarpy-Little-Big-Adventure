using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI_System
{
    public class SettingsUI : MonoBehaviour
    {
        private UI_AudioController _audioController;
        private List<UIAudioSlider> _sliders;

        private void Start()
        {
            _sliders = new List<UIAudioSlider>();
            _sliders.AddRange(GetComponentsInChildren<UIAudioSlider>());

            _audioController = new UI_AudioController();
            _audioController.AddSliders(_sliders);
        }

        private void OnEnable()
        {
            _audioController?.UpdateVolumeValues();
        }
    }
}