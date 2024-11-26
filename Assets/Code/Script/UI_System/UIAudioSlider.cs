using System;
using UnityEngine;
using UnityEngine.UI;

using Audio_System.Channel;

namespace UI_System
{
    [RequireComponent(typeof(Slider))]
    public class UIAudioSlider : MonoBehaviour
    {
        public Action<AudioChannelType, float> OnVolumeChanged;

        [SerializeField] private AudioChannelType _channelType;
        
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(ValueChanged);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(ValueChanged);
        }

        public void SetVolume(float volume) => _slider.value = volume;
        private void ValueChanged(float volume) => OnVolumeChanged?.Invoke(_channelType, volume);

        public AudioChannelType AudioChannel => _channelType;
    }
}
