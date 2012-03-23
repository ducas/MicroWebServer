using System;
using System.IO;

namespace MicroWebServer
{
    public interface ISerializer
    {
        void Serialize(object value, StreamWriter writer);
        object Desrialize(StreamReader reader);
    }
}
