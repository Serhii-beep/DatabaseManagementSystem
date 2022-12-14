using DatabaseManagementSystem.BL.JsonHelpers;
using Newtonsoft.Json;

namespace DatabaseManagementSystem.BL.Fields
{
    [JsonConverter(typeof(FieldConverter))]
    public abstract class Field
    {
        public string Type { get; protected set; }
        public string Name { get; protected set; }
        public string DefaultValue { get; protected set; }
        public Field(string name)
        {
            Name = name;
        }
        public abstract bool IsValid(string value);
    }
}
