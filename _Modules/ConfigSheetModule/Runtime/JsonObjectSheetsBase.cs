using Newtonsoft.Json;
using System.Collections.Generic;

namespace DivineSkies.Modules.Config
{
    public abstract class JsonObjectSheetsBase<TData> : ConfigSheetsBase
    {
        protected Dictionary<string, TData> SheetsData;

        internal sealed override ConfigDataType _dataType => ConfigDataType.JsonObject;

        protected sealed override void DeserializeFileContent(string fileId, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                this.PrintError("Failed to read json data for " + fileId);
                return;
            }

            SheetsData.Add(fileId, JsonConvert.DeserializeObject<TData>(content));
            if (SheetsData[fileId] == null)
            {
                this.PrintError("Failed to deserialize json data for " + fileId);
            }
        }
    }
}