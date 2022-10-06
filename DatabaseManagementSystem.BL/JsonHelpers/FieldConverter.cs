using DatabaseManagementSystem.BL.Fields;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DatabaseManagementSystem.BL.JsonHelpers
{
    public class FieldConverter : JsonConverter
    {
        private static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings()
        {
            ContractResolver = new FieldSpecifiedConcreteClassConverter()
        };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Field));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            string name = (string)jo["Name"];
            switch(jo["Type"].Value<string>())
            {
                case "Char":
                    return JsonConvert.DeserializeObject<CharField>(jo.ToString(), SpecifiedSubclassConversion);
                case "Integer":
                    return JsonConvert.DeserializeObject<IntegerField>(jo.ToString(), SpecifiedSubclassConversion);
                case "Money":
                    return JsonConvert.DeserializeObject<MoneyField>(jo.ToString(), SpecifiedSubclassConversion);
                case "MoneyInterval":
                    return JsonConvert.DeserializeObject<MoneyIntervalField>(jo.ToString(), SpecifiedSubclassConversion);
                case "Real":
                    return JsonConvert.DeserializeObject<RealField>(jo.ToString(), SpecifiedSubclassConversion);
                case "String":
                    return JsonConvert.DeserializeObject<StringField>(jo.ToString(), SpecifiedSubclassConversion);
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
