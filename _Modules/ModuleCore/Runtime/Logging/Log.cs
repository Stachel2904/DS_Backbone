#if !UNITY_EDITOR
using System;
using System.IO;
#endif
using UnityEngine;

namespace DivineSkies.Modules.Logging
{
    internal enum MessageType { ScreenMessage, LogMessage, Log, Warning, Error }

    /// <summary>
    /// Use this class to print messages to Logs
    /// </summary>
    public class Log : ModuleBase<Log>
    {
        /// <summary>
        /// Use this to display ScreenMessages ingame
        /// </summary>
        public event Action<string> OnScreenMessagePrinted;

        /// <summary>
        /// Use this to display LogMessages ingame
        /// </summary>
        public event Action<string> OnLogMessagePrinted;

        /// <summary>
        /// Prints a message to unity console or text file if not in editor.
        /// </summary>
        internal void PrintLogMessage(string sender, string message, MessageType type)
        {
#if UNITY_EDITOR
            sender = $"<b><color=orange>[{sender}]</color></b> ";
            if (type is MessageType.ScreenMessage or MessageType.LogMessage or MessageType.Log)
            {
                Debug.Log(sender + message);
            }

            if (type == MessageType.Warning)
            {
                Debug.LogWarning(sender + message);
            }

            if (type == MessageType.Error)
            {
                Debug.LogError(sender + message);
            }
#else
            sender = $"[{sender}] ";
            AddLogLine($"[{DateTime.Now:HH:mm:ss}] {type.ToString().ToUpper()}: " + sender + message);
#endif
            if(type is MessageType.ScreenMessage)
            {
                OnScreenMessagePrinted?.Invoke(message);
            }
            else if (type is MessageType.ScreenMessage)
            {
                OnLogMessagePrinted?.Invoke(message);
            }
        }

#if !UNITY_EDITOR
        private void AddLogLine(string message)
        {
            DateTime startTime = DateTime.Now.AddSeconds(Time.realtimeSinceStartup * -1);
            string path = $@"{Application.persistentDataPath}/Logs/{startTime.ToString().Replace(".", "").Replace(":", "").Replace(" ", "_")}.txt";

            if (!Directory.Exists($@"{Application.persistentDataPath}/Logs"))
            {
                Directory.CreateDirectory($@"{Application.persistentDataPath}/Logs");
            }

            StreamWriter writer = File.Exists(path) ? new StreamWriter(path, true) : new StreamWriter(File.Create(path));
            writer.WriteLine(message);
            writer.Close();
        }
#endif
    }
}