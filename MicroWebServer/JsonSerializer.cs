using System;
using Microsoft.SPOT;
using System.Collections;
using System.IO;
using System.Reflection;

namespace MicroWebServer
{
    public class JsonSerializer : ISerializer
    {
        public void Serialize(object value, StreamWriter writer)
        {
            if (value == null)
            {
                writer.Write("null");
            }
            else if (value is string)
            {
                writer.Write("\"" + StringHelpers.Replace((string)value, "\"", "\\\"") + "\"");
            }
            else if (IsSimpleType(value))
            {
                writer.Write("\"" + value + "\"");
            }
            else if (value is IEnumerable || value is Array)
            {
                var enumerable = value is Array ? (Array)value : (IEnumerable)value;
                writer.Write("[");
                bool first = true;
                foreach (var item in enumerable)
                {
                    if (!first) writer.Write(", ");
                    Serialize(item, writer);
                    first = false;
                }
                writer.Write("]");
            }
            else
            {
                writer.Write("{ ");
                var type = value.GetType();
                var properties = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                var first = true;
                foreach (var property in properties)
                {
                    var name = property.Name;
                    if (name.IndexOf("get_") != 0) continue;
                    if (!first) writer.Write(", ");

                    name = name.Substring(4);
                    name = name.Substring(0, 1).ToLower() + name.Substring(1);

                    var propertyValue = property.Invoke(value, new object[] { });

                    writer.Write("\"" + name + "\": ");
                    Serialize(propertyValue, writer);

                    first = false;
                }
                writer.Write(" }");
            }
        }

        private bool IsSimpleType(object value)
        {
            return value.GetType().IsValueType;
        }

        public object Desrialize(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
