using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Audio_System;
using Audio_System.Channel;

namespace UI_System
{
    public class UI_AudioController
    {
        private List<UIAudioSlider> _audioSliders;

        public void AddSliders(List<UIAudioSlider> sliders)
        {
            _audioSliders = new List<UIAudioSlider>();
            _audioSliders.AddRange(sliders);
            LoadVolumeValues();

            for (int i = 0; i < _audioSliders.Count; i++)
                _audioSliders[i].OnVolumeChanged += AudioManager.SetVolume;

        }

        private void LoadVolumeValues()
        {
            for(int i = 0;i < _audioSliders.Count;i++)
            {
                AudioChannelType channelType = _audioSliders[i].AudioChannel;
                _audioSliders[i].SetVolume(AudioManager.GetVolume(channelType));
            }
        }
    }
}
