using System;
using System.Collections.Generic;
using DivineSkies.Modules.Config;

public static class Sheet
{
    public static T Get<T>() where T : ConfigSheetBase, new() => ConfigController.Main.GetSheet<T>();
}

namespace DivineSkies.Modules.Config
{
    public class ConfigController : ModuleBase<ConfigController>
    {
        private readonly Dictionary<Type, ConfigSheetBase> _loadedConfigs = new();

        public T GetSheet<T>() where T : ConfigSheetBase, new()
        {
            if (_loadedConfigs.TryGetValue(typeof(T), out ConfigSheetBase sheet))
                return (T)sheet;

            ConfigSheetBase result = new T();
            _loadedConfigs.Add(typeof(T), result);
            return (T)result;
        }
    }
}
