using System;

namespace ImitateDance.Scripts.Applications.Enums
{
    public enum MusicDifficulty
    {
        Easy,
        Normal,
        Hard
    }

    public static class MusicDifficultyExtension
    {
        public static MusicDifficulty Next(this MusicDifficulty self)
        {
            var o = self + 1;
            return Enum.IsDefined(typeof(MusicDifficulty), o) ? o : MusicDifficulty.Easy;
        }

        public static MusicDifficulty Previous(this MusicDifficulty self)
        {
            var o = self - 1;
            return Enum.IsDefined(typeof(MusicDifficulty), o) ? o : MusicDifficulty.Hard;
        }
    }
}