using System.Collections.Generic;
using Newtonsoft.Json;

namespace DivineSkies.Modules.Config
{
    public abstract class JsonArraySheetsBase<TData> : ConfigSheetsBase
    {
        protected Dictionary<string, List<TData>> SheetsDatas;

        internal sealed override ConfigDataType _dataType => ConfigDataType.JsonArray;

        protected sealed override void DeserializeFileContent(string fileId, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                this.PrintError("Failed to read json data for " + fileId);
                return;
            }

            JsonArrayWrapper wrapper = JsonConvert.DeserializeObject<JsonArrayWrapper>(content);
            if (wrapper == null)
            {
                this.PrintError("Failed to deserialize json data for " + fileId);
            }

            SheetsDatas.Add(fileId, new List<TData>(wrapper.Elements));
        }

        private class JsonArrayWrapper
        {
            public TData[] Elements;
        }
    }
}