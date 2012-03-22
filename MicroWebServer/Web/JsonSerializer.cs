using System;
using Microsoft.SPOT;
using System.Collections;
using System.IO;
using System.Reflection;

namespace MicroWebServer.Web
{
    public class JsonSerializer : ISerializer
    {
        public void Serialize(object value, StreamWriter writer)
        {
            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
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
                    if (propertyValue is string) propertyValue = Escape(propertyValue);

                    writer.Write("\"" + name + "\": \"" + propertyValue + "\"");

                    first = false;
                }
                writer.Write(" } ");
            }
        }

        private object Escape(object propertyValue)
        {
            var value = propertyValue as string;
            if (value == null || value == "") return propertyValue;

            var last = 0;
            while ((last = value.IndexOf('"', last + 2)) > -1)
                value = value.Substring(0, last) + "\\\"" + value.Substring(last + 1);

            return value;
        }

        public object Desrialize(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
