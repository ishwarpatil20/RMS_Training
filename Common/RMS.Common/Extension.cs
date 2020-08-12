using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common
{
    public static class Extension
    {
        public static List<T> SqlReaderToList<T>(this SqlDataReader reader) where T : class, new()
        {
            var list = new List<T>();

            if (reader == null)
                return list;

            HashSet<string> tableColumnNames = null;
            while (reader.Read())
            {
                tableColumnNames = tableColumnNames ?? CollectColumnNames(reader);
                var entity = new T();
                foreach (var propertyInfo in typeof(T).GetProperties())
                {
                    object retrievedObject = null;
                    if (tableColumnNames.Contains(propertyInfo.Name) && (retrievedObject = reader[propertyInfo.Name]) != null)
                    {
                        var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

                        SetValue(entity, propertyInfo.Name, retrievedObject);

                        //propertyInfo.SetValue(entity, retrievedObject, null);

                        
                    }
                }
                list.Add(entity);
            }
            return list;
        }
        public static void SetValue(object inputObject, string propertyName, object propertyVal)
        {
            //find out the type
            Type type = inputObject.GetType();

            //get the property information based on the type
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);

            //find the property type
            Type propertyType = propertyInfo.PropertyType;

            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

            //Returns an System.Object with the specified System.Type and whose value is
            //equivalent to the specified object.
            propertyVal = Convert.ChangeType(propertyVal, targetType);

            //Set the value of the property
            propertyInfo.SetValue(inputObject, propertyVal, null);

        }
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
        static HashSet<string> CollectColumnNames(SqlDataReader reader)
        {
            var collectedColumnInfo = new HashSet<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                collectedColumnInfo.Add(reader.GetName(i));
            }
            return collectedColumnInfo;
        }
    }
}
