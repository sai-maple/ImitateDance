using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Intro
{
    public sealed class TextMakerView : Marker, INotification
    {
        public string Message;

        public PropertyName id => new PropertyName("method");
    }
}