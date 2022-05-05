using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    public sealed class AudioView : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _audioSources = default;
        private AudioClip _clip = default;
        private float _volume = 1f;
        private int _index = default;

        private void Awake()
        {
            foreach (var audioSource in _audioSources)
            {
                audioSource.volume = 0;
            }
        }

        public async UniTask Load(string assetName, CancellationToken token)
        {
            _clip = await Addressables.LoadAssetAsync<AudioClip>(assetName).WithCancellation(token);
        }

        public void Play()
        {
            var current = _audioSources[_index];
            current.DOFade(0, 1).OnComplete(() => current.Stop());
            _index++;
            _audioSources[_index].loop = true;
            _audioSources[_index].clip = _clip;
            _audioSources[_index].Play();
            _audioSources[_index].DOFade(_volume, 1);
        }

        public void Stop()
        {
            var current = _audioSources[_index];
            current.DOFade(0, 1).OnComplete(() => current.Stop());
        }

        public void PlayGameMusic()
        {
            _audioSources[_index].clip = _clip;
            _audioSources[_index].volume = _volume;
            _audioSources[_index].Play();
            _audioSources[_index].loop = false;
        }

        public void OnChangeVolume(float value)
        {
            _volume = value;
            _audioSources[_index].volume = value;
        }
    }
}