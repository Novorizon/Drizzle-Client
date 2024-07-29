public static class Log
{
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    static public void Warning(object message)
    {
        UnityEngine.Debug.LogWarning(message);
    }
}
