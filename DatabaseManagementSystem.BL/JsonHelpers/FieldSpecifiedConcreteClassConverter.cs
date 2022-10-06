using DatabaseManagementSystem.BL.Fields;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatabaseManagementSystem.BL.JsonHelpers
{
    public class FieldSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter? ResolveContractConverter(Type objectType)
        {
            if(typeof(Field).IsAssignableFrom(objectType) && !objectType.IsAbstract)
            {
                return null;
            }
            return base.ResolveContractConverter(objectType);
        }
    }
}
