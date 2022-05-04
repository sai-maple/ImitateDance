using UnityEngine;

namespace ImitateDance.Scripts.Applications.Common
{
    public static class Logger
    {
        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }
    }
}