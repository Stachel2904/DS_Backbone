using DivineSkies.Modules;
using DivineSkies.Modules.Logging;
using System;

public static class PrintExtesions
{
    /// <summary>
    /// Use this to log ingame Messages
    /// </summary>
    /// <param name="onScreen">Is this an on screen message?</param>
    public static void PrintMessage(this object obj, string message, bool onScreen = true) => Print(obj, message, onScreen ? MessageType.ScreenMessage : MessageType.LogMessage);

    /// <summary>
    /// Use this to print a normal Log to console (in editor) or file (in build)
    /// </summary>
    public static void PrintLog(this object obj, object msgObj) => Print(obj, msgObj.ToString(), MessageType.Log);

    /// <summary>
    /// Use this to print a normal Log to console (in editor) or file (in build)
    /// </summary>
    public static void PrintLog(this object obj, string message) => Print(obj, message, MessageType.Log);

    /// <summary>
    /// Use this to print a Warning to console (in editor) or file (in build)
    /// </summary>
    public static void PrintWarning(this object obj, object msgObj) => Print(obj, msgObj.ToString(), MessageType.Warning);

    /// <summary>
    /// Use this to print a Warning to console (in editor) or file (in build)
    /// </summary>
    public static void PrintWarning(this object obj, string message) => Print(obj, message, MessageType.Warning);

    /// <summary>
    /// Use this to print a Error to console (in editor) or file (in build)
    /// </summary>
    public static void PrintError(this object obj, object msgObj) => Print(obj, msgObj.ToString(), MessageType.Error);

    /// <summary>
    /// Use this to print a Error to console (in editor) or file (in build)
    /// </summary>
    public static void PrintError(this object obj, string message) => Print(obj, message, MessageType.Error);

    private static void Print(object obj, string message, MessageType type = MessageType.Log)
    {
        string className = obj.GetType().ToString().Remove(0, obj.GetType().ToString().LastIndexOf('.') + 1);
        if (!ModuleController.Has<Log>())
        {
            throw new NullReferenceException(message);
        }
        Log.Main.PrintLogMessage(className, message, type);
    }
}