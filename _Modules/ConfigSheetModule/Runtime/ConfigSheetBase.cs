using System.IO;

namespace DivineSkies.Modules.Config
{
    internal enum ConfigDataType
    {
        Csv,
        JsonObject,
        JsonArray
    }

    public abstract class ConfigSheetBase
    {
        protected abstract string _fileName { get; }
        internal abstract ConfigDataType _dataType { get; }

        internal ConfigSheetBase()
        {
            string fileContent = File.ReadAllText(CreatePathString());
            DeserializeFileContent(fileContent);
        }

        private string CreatePathString() => $@"Assets/Config/{_dataType}/{_fileName}." + _dataType switch
        {
            ConfigDataType.JsonObject or ConfigDataType.JsonArray => ".json",
            _ => _dataType.ToString().ToLower()
        };

        protected abstract void DeserializeFileContent(string content);
    };
}