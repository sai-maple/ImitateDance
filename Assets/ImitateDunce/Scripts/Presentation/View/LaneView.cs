using System.Collections.Generic;
using System.Linq;
using ImitateDunce.Applications.Data;
using ImitateDunce.Applications.Enums;
using UnityEngine;

namespace ImitateDunce.Presentation.View
{
    public sealed class LaneView : MonoBehaviour
    {
        [SerializeField] private List<NoteView> _noteViews = default;
        [SerializeField] private RectTransform _content = default;
        [SerializeField] private RectTransform _currentLine = default;

        private Vector2 _position = default;

        // 楽譜データを渡して表示する
        public void Initialize(ScoreDto scoreDto)
        {
            for (var i = 0; i < _noteViews.Count; i++)
            {
                _noteViews[i].Initialize(scoreDto.Score.Any(note => note.Beat == i));
            }
        }

        // ターン交代中に、変化したNoteを隠す
        public void HideAll()
        {
            foreach (var note in _noteViews)
            {
                note.Hide();
            }
        }

        // ターンの音楽再生中、再生位位置に線を移動させる
        public void Play(float normalize)
        {
            _position.x = _content.sizeDelta.x * normalize;
            _currentLine.anchoredPosition = _position;
        }

        // 入力を受けて、タイミングに応じたNoteを変化させる
        public void Dunce(int beet, DunceDirection direction, bool isSuccess)
        {
            _noteViews[beet].Judge(direction, isSuccess);
        }
    }
}