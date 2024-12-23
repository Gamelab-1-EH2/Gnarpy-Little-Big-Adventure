using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using Audio_System.Channel;
using Audio_System.SFX;

namespace Audio_System
{
    [RequireComponent(typeof(SFXManager))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Settings")]
        [SerializeField] private AudioManager_SO _settings;

        private List<AudioChannel> _audioData_List;
        private static AudioMixer _mixer;

        private void Awake()
        {
            //Singleton set up
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            else
                Instance = this;

            SetUpAudio();
        }
        
        private void SetUpAudio()
        {
            _mixer = _settings.Mixer;
            _audioData_List = _settings.AudioData_List;
            //Audio is saved in decimal scale an then converted
            float volume = 0f;
            for (int i = 0; i < _audioData_List.Count; i++)
            {
                volume = _audioData_List[i].DefaultVolume;
                //Player Prefs
                if (!PlayerPrefs.HasKey(_audioData_List[i].Type.ToString() + "Volume"))
                    PlayerPrefs.SetFloat(_audioData_List[i].Type.ToString() + "Volume", _audioData_List[i].DefaultVolume);
                else
                    volume = PlayerPrefs.GetFloat(_audioData_List[i].Type.ToString() + "Volume");

                //Convert audio volume
                volume = 20 * Mathf.Log10(volume);
                if (volume < -80f)
                    volume = -80f;
                //Set Mixer volume
                _mixer.SetFloat(_audioData_List[i].Type.ToString() + "Volume", volume);
            }
        }

        /// <summary>
        /// Change audio volume and save it into prefs
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="volume"></param>
        public static void ChangeVolume(AudioChannelType channel, float volume)
        {
            //Update Player Prefs
            PlayerPrefs.SetFloat(channel.ToString() + "Volume", volume);

            //Convert audio volume
            volume = 20 * Mathf.Log10(volume);
            if (volume < -80f)
                volume = -80f;
            //Set Mixer volume
            _mixer.SetFloat(channel.ToString() + "Volume", volume);
        }
    }
}
