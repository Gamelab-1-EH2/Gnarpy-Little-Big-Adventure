using UnityEngine;

namespace Audio_System.Channel
{
    [System.Serializable]
    public class AudioChannel
    {
        [SerializeField] public AudioChannelType Type;
        [SerializeField][Range(0f, 1f)] public float DefaultVolume = 0.75f;
    }

    /// <summary>
    /// Audio Enum Channels
    /// </summary>
    public enum AudioChannelType
    {
        Master,
        Music,
        SFX
    }
}
