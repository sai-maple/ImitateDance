using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ImitateDance.Scripts.Presentation.View.AudioTrack
{
    public sealed class AudioMakerView: Marker, INotification
    {
        public AudioClip Clip;
        public float Duration;
        public bool Loop;

        public PropertyName id => new PropertyName("method");
    }
}