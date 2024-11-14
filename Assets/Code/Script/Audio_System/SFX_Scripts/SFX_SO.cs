using System.Collections.Generic;
using UnityEngine;

namespace Audio_System.SFX
{
    [CreateAssetMenu(fileName = "New SFX", menuName = "Settings/Audio/SFX/SFX")]
    public class SFX_SO : ScriptableObject
    {
        [SerializeField] public List<AudioClip> Clips = null;
        [SerializeField] public float StartingPitch = 1f;
        [SerializeField] public float PitchVariation = 0f;
        [SerializeField, Range(0f, 1f)] public float SpatialBlend = 1f;

        public SFX GetSFX()
        {
            SFX sfx = new SFX(Clips[UnityEngine.Random.Range(0, Clips.Count)], StartingPitch + UnityEngine.Random.Range(-PitchVariation, +PitchVariation), SpatialBlend);
            return sfx;
        }
    }
}
