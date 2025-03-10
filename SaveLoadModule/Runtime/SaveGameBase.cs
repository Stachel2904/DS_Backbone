using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DivineSkies.Modules.SaveGame
{
    /// <summary>
    /// Use this class for you Data-Storage
    /// </summary>
    public class SaveGameBase
    {
        internal long CreationTimeStamp;

        public SaveGameBase()
        {
            CreationTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        internal string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new StringEnumConverter());
        }
    }
}
