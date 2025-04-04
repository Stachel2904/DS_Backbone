using System.Collections.Generic;
using Newtonsoft.Json;

namespace DivineSkies.Modules.Config
{
    public abstract class JsonArraySheetBase<TData> : ConfigSheetBase
    {
        protected List<TData> SheetDatas;

        internal sealed override ConfigDataType _dataType => ConfigDataType.JsonArray;

        protected sealed override void DeserializeFileContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                this.PrintError("Failed to read json data");
                return;
            }

            JsonArrayWrapper wrapper = JsonConvert.DeserializeObject<JsonArrayWrapper>(content);
            if (wrapper == null)
            {
                this.PrintError("Failed to deserialize json data");
            }

            SheetDatas = new List<TData>(wrapper.Elements);
        }

        private class JsonArrayWrapper
        {
            public TData[] Elements;
        }
    }
}