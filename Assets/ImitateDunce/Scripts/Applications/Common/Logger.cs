using UnityEngine;

namespace ImitateDunce.Applications.Common
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