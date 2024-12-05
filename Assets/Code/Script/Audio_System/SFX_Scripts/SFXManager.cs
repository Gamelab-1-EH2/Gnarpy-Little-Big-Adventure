using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio_System.SFX
{
    /// <summary>
    /// Singleton manager used to reproduce and manage SFX
    /// </summary>
    public class SFXManager : MonoBehaviour
    {
        public static Action<SFX, Vector3> PlaySFX = null;

        [SerializeField] private SFXManager_SO _SFX_so;
        private List<AudioSource> _sources = new List<AudioSource>();

        private void Awake()
        {
            //Instanciate SFX Objects
            AudioSource source = _SFX_so.AudioSource;
            for (int i = 0; i < _SFX_so.AudioSourcesAmount; i++)
                _sources.Add(Instantiate(source, transform));

            //Connect event
            PlaySFX += Request_SFX;
        }

        /// <summary>
        /// Play an SFX in a specified position
        /// </summary>
        /// <param name="sfx"></param>
        /// <param name="position"></param>
        private void Request_SFX(SFX sfx, Vector3 position)
        {
            if (sfx == null)
                return;
            if(sfx.Clip == null)
                return;

            //Find first available AudioSource
            AudioSource source = GetFreeAudioSource();
            //Apply SFX to audio source
            source.transform.position = position;
            source.pitch = sfx.Pitch;
            source.clip = sfx.Clip;
            source.spatialBlend = sfx.SpatialBlend;
            source.Play(0);
        }

        /// <summary>
        /// Returns the first free AudioSource, if no is free add new one
        /// </summary>
        private AudioSource GetFreeAudioSource()
        {
            for (int i = 0; i < _sources.Count; i++)
                if (!_sources[i].isPlaying)
                    return _sources[i];

            //Add new audio source
            Debug.LogWarning("SFX MANAGER - Obligated to instanciate SFX audio source, consider incrementing max SFX amount", this.gameObject);
            AudioSource source = _SFX_so.AudioSource;
            _sources.Add(Instantiate(source, transform));

            return _sources[^1];
        }
    }
}