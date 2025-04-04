using Newtonsoft.Json;

namespace DivineSkies.Modules.Config
{
    public abstract class JsonObjectSheetBase<TData> : ConfigSheetBase
    {
        protected TData SheetData;

        internal sealed override ConfigDataType _dataType => ConfigDataType.JsonObject;

        protected sealed override void DeserializeFileContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                this.PrintError("Failed to read json data");
                return;
            }

            SheetData = JsonConvert.DeserializeObject<TData>(content);
            if (SheetData == null)
            {
                this.PrintError("Failed to deserialize json data");
            }
        }
    }
}