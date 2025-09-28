using System;
using System.Collections.Generic;
using DivineSkies.Modules.Config;

public static class Sheet
{
    public static T Get<T>() where T : IConfigSheet, new() => ConfigController.Main.GetSheet<T>();
}

namespace DivineSkies.Modules.Config
{
    public class ConfigController : ModuleBase<ConfigController>
    {
        private readonly Dictionary<Type, IConfigSheet> _loadedConfigs = new();

        public T GetSheet<T>() where T : IConfigSheet, new()
        {
            if (_loadedConfigs.TryGetValue(typeof(T), out IConfigSheet sheet))
                return (T)sheet;

            IConfigSheet result = new T();
            _loadedConfigs.Add(typeof(T), result);
            return (T)result;
        }
    }
}
