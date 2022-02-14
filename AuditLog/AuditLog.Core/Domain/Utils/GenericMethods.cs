using Newtonsoft.Json;
using System.Linq;
using System.Reflection;

namespace AuditLog.Core.Domain.Utils
{
    public static class GenericMethods
    {
        public static bool IsNullOrEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNull(object data)
        {
            return data == null;
        }

        public static bool InsertIsValid(object data)
        {
            var propertyInfo = GetPropertyInfoId(data);
            var id = GetPropertyValue(propertyInfo, data);

            return ContainId(id);
        }

        private static PropertyInfo GetPropertyInfoId(object data)
        {
            return data.GetType().GetProperties().FirstOrDefault(x => x.Name == "Id");
        }

        private static int GetPropertyValue(PropertyInfo propertyInfo, object data)
        {
            return (int)propertyInfo.GetValue(data, null);
        }

        public static bool ContainId(int? id)
        {
            return id.HasValue && id.Value > 0;
        }

        public static string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
        
        public static bool DifferentObjects(object objectOne, object objectTwo)
        {
            return SerializeObject(objectOne) != SerializeObject(objectTwo);
        }
    }
}
