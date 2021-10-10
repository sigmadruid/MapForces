using System.Diagnostics;

namespace Game.Scripts.FrameWork
{
    public static class Logger
    {
        private const string DEBUG_SYMBOL = "LOG_ENABLE";

        [Conditional(DEBUG_SYMBOL)]
        public static void Log(object content)
        {
            UnityEngine.Debug.Log(content);
        }
        
        [Conditional(DEBUG_SYMBOL)]
        public static void LogFormat(object content, params object[] args)
        {
            UnityEngine.Debug.LogFormat(content.ToString(), args);
        }
        
        [Conditional(DEBUG_SYMBOL)]
        public static void Warn(object content)
        {
            UnityEngine.Debug.LogWarning(content);
        }
        
        [Conditional(DEBUG_SYMBOL)]
        public static void WarnFormat(object content, params object[] args)
        {
            UnityEngine.Debug.LogWarningFormat(content.ToString(), args);
        }
        
        [Conditional(DEBUG_SYMBOL)]
        public static void Error(object content)
        {
            UnityEngine.Debug.LogError(content);
        }
        
        [Conditional(DEBUG_SYMBOL)]
        public static void ErrorFormat(object content, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(content.ToString(), args);
        }
    }
}