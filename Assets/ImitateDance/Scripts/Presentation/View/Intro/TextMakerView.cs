using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ImitateDance.Scripts.Presentation.View.Intro
{
    public sealed class TextMakerView : Marker, INotification
    {
        public string Message;

        public PropertyName id => new PropertyName("method");
    }
}