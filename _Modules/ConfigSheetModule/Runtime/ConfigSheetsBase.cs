using System.IO;

namespace DivineSkies.Modules.Config
{
    public abstract class ConfigSheetsBase : IConfigSheet
    {
        protected abstract string DirectoryName { get; }
        internal abstract ConfigDataType _dataType { get; }

        internal ConfigSheetsBase()
        {
            foreach (string fileName in Directory.GetFiles(DirectoryName))
            {
                string fileContent = File.ReadAllText(CreatePathString(fileName));

                string fileId = fileName.Remove(fileName.LastIndexOf('.')).ToLower();

                DeserializeFileContent(fileId, fileContent);
            }            
        }

        private string CreatePathString(string fileName) => $@"Assets/Config/{_dataType}/{DirectoryName}/{fileName}." + _dataType switch
        {
            ConfigDataType.JsonObject or ConfigDataType.JsonArray => "json",
            _ => _dataType.ToString().ToLower()
        };

        protected abstract void DeserializeFileContent(string fileId, string fileContent);
    };
}