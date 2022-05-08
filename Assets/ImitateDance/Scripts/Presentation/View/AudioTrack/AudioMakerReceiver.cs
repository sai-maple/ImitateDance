using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace ImitateDance.Scripts.Presentation.View.AudioTrack
{
    public sealed class AudioMakerReceiver : MonoBehaviour, INotificationReceiver
    {
        [SerializeField] private AudioSource _audioSource = default;

        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

        public async void OnNotify(Playable origin, INotification notification, object context)
        {
            var element = notification as AudioMakerView;
            if (element == null) return;

            _audioSource.loop = element.Loop;

            if (!element.Loop)
            {
                _audioSource.PlayOneShot(element.Clip);
                return;
            }

            _audioSource.clip = element.Clip;
            _audioSource.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(element.Duration), cancellationToken: _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            _audioSource.Stop();
        }

        private void OnDestroy()
        {
            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}